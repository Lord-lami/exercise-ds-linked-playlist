import static org.junit.jupiter.api.Assertions.assertEquals;
import static org.junit.jupiter.api.Assertions.assertFalse;
import static org.junit.jupiter.api.Assertions.assertNull;
import static org.junit.jupiter.api.Assertions.assertTrue;

import java.util.List;
import java.util.function.IntPredicate;
import org.junit.jupiter.api.Test;

// The tests for your playlist. You don't need to change this file.
// Each message says what was expected, so a failing test tells you what to look at.
class PlaylistTest {
    private static Playlist make(String... songs) {
        Playlist playlist = new Playlist();
        for (String song : songs) {
            playlist.add(song);
        }
        return playlist;
    }

    @Test
    void nextMovesForwardAndStopsAtTheEnd() {
        Playlist playlist = make("Midnight Bus", "Lemon Skies", "Slow Rain");
        assertTrue(playlist.next(), "Not yet: there's a song after Midnight Bus, so next returns true.");
        assertEquals("Lemon Skies", playlist.current(), "Not yet: next moves from Midnight Bus to Lemon Skies.");
        assertTrue(playlist.next(), "Not yet: there's a song after Lemon Skies.");
        assertEquals("Slow Rain", playlist.current(), "Not yet: next moves on to Slow Rain.");
        assertFalse(playlist.next(), "Not yet: Slow Rain is the last song, so next returns false.");
        assertEquals("Slow Rain", playlist.current(), "Not yet: at the last song, next stays where it is.");
        assertFalse(new Playlist().next(), "Not yet: an empty playlist has nowhere to go.");
    }

    @Test
    void previousMovesBackAndStopsAtTheStart() {
        Playlist playlist = make("Midnight Bus", "Lemon Skies", "Slow Rain");
        playlist.next();
        playlist.next();
        assertTrue(playlist.previous(), "Not yet: there's a song before Slow Rain, so previous returns true.");
        assertEquals("Lemon Skies", playlist.current(), "Not yet: previous moves from Slow Rain back to Lemon Skies.");
        assertTrue(playlist.previous(), "Not yet: there's a song before Lemon Skies.");
        assertEquals("Midnight Bus", playlist.current(), "Not yet: previous moves back to Midnight Bus.");
        assertFalse(playlist.previous(), "Not yet: Midnight Bus is the first song, so previous returns false.");
        assertEquals("Midnight Bus", playlist.current(), "Not yet: at the first song, previous stays where it is.");
        assertFalse(new Playlist().previous(), "Not yet: an empty playlist has nowhere to go.");
    }

    @Test
    void addAfterCurrentInTheMiddle() {
        Playlist playlist = make("Midnight Bus", "Lemon Skies", "Slow Rain");
        playlist.addAfterCurrent("Golden Hour");
        assertEquals(List.of("Midnight Bus", "Golden Hour", "Lemon Skies", "Slow Rain"), playlist.titles(),
            "Not yet: Golden Hour goes right after Midnight Bus, the current song.");
        assertEquals(List.of("Slow Rain", "Lemon Skies", "Golden Hour", "Midnight Bus"), playlist.titlesBackward(),
            "Not yet: walking backward, Lemon Skies' prev must now be Golden Hour, and Golden Hour's prev Midnight Bus.");
        assertEquals("Midnight Bus", playlist.current(), "Not yet: adding after the current song doesn't change it.");
        assertEquals(4, playlist.count(), "Not yet: the playlist has 4 songs now.");
    }

    @Test
    void addAfterCurrentAtTheEndMovesTheTail() {
        Playlist playlist = make("Midnight Bus", "Lemon Skies");
        playlist.next();
        playlist.addAfterCurrent("Golden Hour");
        assertEquals(List.of("Midnight Bus", "Lemon Skies", "Golden Hour"), playlist.titles(),
            "Not yet: Golden Hour goes after Lemon Skies, at the end.");
        assertEquals(List.of("Golden Hour", "Lemon Skies", "Midnight Bus"), playlist.titlesBackward(),
            "Not yet: Golden Hour is the last song now, so the tail must point at it.");
        assertTrue(playlist.next(), "Not yet: there's a song after Lemon Skies now.");
        assertEquals("Golden Hour", playlist.current(), "Not yet: next from Lemon Skies reaches Golden Hour.");
    }

    @Test
    void addAfterCurrentOnAnEmptyPlaylist() {
        Playlist playlist = new Playlist();
        playlist.addAfterCurrent("Golden Hour");
        assertEquals(List.of("Golden Hour"), playlist.titles(), "Not yet: on an empty playlist, the new song is the only song.");
        assertEquals(List.of("Golden Hour"), playlist.titlesBackward(), "Not yet: the only song is the head and the tail.");
        assertEquals("Golden Hour", playlist.current(), "Not yet: the only song becomes the current one.");
    }

    @Test
    void removeCurrentInTheMiddle() {
        Playlist playlist = make("Midnight Bus", "Lemon Skies", "Slow Rain");
        playlist.next();
        assertEquals("Lemon Skies", playlist.removeCurrent(), "Not yet: removeCurrent returns the title it took out.");
        assertEquals(List.of("Midnight Bus", "Slow Rain"), playlist.titles(), "Not yet: Midnight Bus' next must skip to Slow Rain.");
        assertEquals(List.of("Slow Rain", "Midnight Bus"), playlist.titlesBackward(), "Not yet: Slow Rain's prev must skip back to Midnight Bus.");
        assertEquals("Slow Rain", playlist.current(), "Not yet: the song after the removed one plays next.");
        assertEquals(2, playlist.count(), "Not yet: 2 songs are left.");
    }

    @Test
    void removeCurrentAtTheFrontAndTheEnd() {
        Playlist playlist = make("Midnight Bus", "Lemon Skies", "Slow Rain");
        assertEquals("Midnight Bus", playlist.removeCurrent(), "Not yet: the current song is Midnight Bus.");
        assertEquals(List.of("Lemon Skies", "Slow Rain"), playlist.titles(), "Not yet: removing the first song moves the head to Lemon Skies.");
        assertEquals("Lemon Skies", playlist.current(), "Not yet: Lemon Skies plays next.");
        playlist.next();
        assertEquals("Slow Rain", playlist.removeCurrent(), "Not yet: the current song is Slow Rain.");
        assertEquals(List.of("Lemon Skies"), playlist.titlesBackward(), "Not yet: removing the last song moves the tail back to Lemon Skies.");
        assertEquals("Lemon Skies", playlist.current(), "Not yet: with no song after it, the song before plays.");
    }

    @Test
    void removeTheOnlySongAndThenNothing() {
        Playlist playlist = make("Midnight Bus");
        assertEquals("Midnight Bus", playlist.removeCurrent(), "Not yet: the only song is the current one.");
        assertEquals(List.of(), playlist.titles(), "Not yet: the playlist is empty now.");
        assertEquals(List.of(), playlist.titlesBackward(), "Not yet: an empty playlist has no tail either.");
        assertNull(playlist.current(), "Not yet: nothing is playing on an empty playlist.");
        assertNull(playlist.removeCurrent(), "Not yet: an empty playlist has nothing to remove.");
    }

    @Test
    void aLongUpNextQueueStaysFast() {
        Playlist playlist = new Playlist();
        for (int i = 0; i < 100000; i++) {
            playlist.add("Song " + i);
        }
        long started = System.nanoTime();
        IntPredicate tooSlow = i -> i % 1000 == 0 && System.nanoTime() - started > 2_000_000_000L;

        for (int i = 0; i < 50000; i++) {
            playlist.next();
        }
        for (int i = 0; i < 100000; i++) {
            playlist.addAfterCurrent("Next " + i);
            if (tooSlow.test(i)) {
                break;
            }
        }
        int forward = 0;
        while (playlist.next()) {
            forward++;
            if (tooSlow.test(forward)) {
                break;
            }
        }
        int back = 0;
        while (playlist.previous()) {
            back++;
            if (tooSlow.test(back)) {
                break;
            }
        }
        for (int i = 0; i < 100000; i++) {
            playlist.removeCurrent();
            if (tooSlow.test(i)) {
                break;
            }
        }
        double seconds = (System.nanoTime() - started) / 1e9;
        assertTrue(seconds < 2, "Not yet: the long up-next queue ran past 2 seconds: it was stopped at %.1f seconds. When next, previous, play next and remove each change a few links, the whole run takes well under one second.".formatted(seconds));
        assertEquals(149999, forward, "Not yet: after 100,000 songs go in after Song 50000, next steps 149,999 times to the last song.");
        assertEquals(199999, back, "Not yet: previous steps 199,999 times, back to the first song.");
        assertEquals(100000, playlist.count(), "Not yet: 100,000 of the 200,000 songs were removed.");
        assertEquals("Next 50000", playlist.current(), "Not yet: after the first 100,000 songs are removed from the front, Next 50000 plays.");
    }
}
