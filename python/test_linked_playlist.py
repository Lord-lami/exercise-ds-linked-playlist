# The tests for your playlist. You don't need to change this file.
# Each message says what was expected, so a failing test tells you what to look at.
import time

from linked_playlist import Playlist


def make(*songs):
    playlist = Playlist()
    for song in songs:
        playlist.add(song)
    return playlist


def test_next_moves_forward_and_stops_at_the_end():
    playlist = make("Midnight Bus", "Lemon Skies", "Slow Rain")
    assert playlist.next() is True, "Not yet: there's a song after Midnight Bus, so next returns True."
    assert playlist.current == "Lemon Skies", "Not yet: next moves from Midnight Bus to Lemon Skies."
    assert playlist.next() is True, "Not yet: there's a song after Lemon Skies."
    assert playlist.current == "Slow Rain", "Not yet: next moves on to Slow Rain."
    assert playlist.next() is False, "Not yet: Slow Rain is the last song, so next returns False."
    assert playlist.current == "Slow Rain", "Not yet: at the last song, next stays where it is."
    assert Playlist().next() is False, "Not yet: an empty playlist has nowhere to go."


def test_previous_moves_back_and_stops_at_the_start():
    playlist = make("Midnight Bus", "Lemon Skies", "Slow Rain")
    playlist.next()
    playlist.next()
    assert playlist.previous() is True, "Not yet: there's a song before Slow Rain, so previous returns True."
    assert playlist.current == "Lemon Skies", "Not yet: previous moves from Slow Rain back to Lemon Skies."
    assert playlist.previous() is True, "Not yet: there's a song before Lemon Skies."
    assert playlist.current == "Midnight Bus", "Not yet: previous moves back to Midnight Bus."
    assert playlist.previous() is False, "Not yet: Midnight Bus is the first song, so previous returns False."
    assert playlist.current == "Midnight Bus", "Not yet: at the first song, previous stays where it is."
    assert Playlist().previous() is False, "Not yet: an empty playlist has nowhere to go."


def test_add_after_current_in_the_middle():
    playlist = make("Midnight Bus", "Lemon Skies", "Slow Rain")
    playlist.add_after_current("Golden Hour")
    assert playlist.titles() == ["Midnight Bus", "Golden Hour", "Lemon Skies", "Slow Rain"], \
        "Not yet: Golden Hour goes right after Midnight Bus, the current song."
    assert playlist.titles_backward() == ["Slow Rain", "Lemon Skies", "Golden Hour", "Midnight Bus"], \
        "Not yet: walking backward, Lemon Skies' prev must now be Golden Hour, and Golden Hour's prev Midnight Bus."
    assert playlist.current == "Midnight Bus", "Not yet: adding after the current song doesn't change it."
    assert playlist.count == 4, "Not yet: the playlist has 4 songs now."


def test_add_after_current_at_the_end_moves_the_tail():
    playlist = make("Midnight Bus", "Lemon Skies")
    playlist.next()
    playlist.add_after_current("Golden Hour")
    assert playlist.titles() == ["Midnight Bus", "Lemon Skies", "Golden Hour"], \
        "Not yet: Golden Hour goes after Lemon Skies, at the end."
    assert playlist.titles_backward() == ["Golden Hour", "Lemon Skies", "Midnight Bus"], \
        "Not yet: Golden Hour is the last song now, so the tail must point at it."
    assert playlist.next() is True, "Not yet: there's a song after Lemon Skies now."
    assert playlist.current == "Golden Hour", "Not yet: next from Lemon Skies reaches Golden Hour."


def test_add_after_current_on_an_empty_playlist():
    playlist = Playlist()
    playlist.add_after_current("Golden Hour")
    assert playlist.titles() == ["Golden Hour"], "Not yet: on an empty playlist, the new song is the only song."
    assert playlist.titles_backward() == ["Golden Hour"], "Not yet: the only song is the head and the tail."
    assert playlist.current == "Golden Hour", "Not yet: the only song becomes the current one."


def test_remove_current_in_the_middle():
    playlist = make("Midnight Bus", "Lemon Skies", "Slow Rain")
    playlist.next()
    assert playlist.remove_current() == "Lemon Skies", "Not yet: remove_current returns the title it took out."
    assert playlist.titles() == ["Midnight Bus", "Slow Rain"], "Not yet: Midnight Bus' next must skip to Slow Rain."
    assert playlist.titles_backward() == ["Slow Rain", "Midnight Bus"], "Not yet: Slow Rain's prev must skip back to Midnight Bus."
    assert playlist.current == "Slow Rain", "Not yet: the song after the removed one plays next."
    assert playlist.count == 2, "Not yet: 2 songs are left."


def test_remove_current_at_the_front_and_the_end():
    playlist = make("Midnight Bus", "Lemon Skies", "Slow Rain")
    assert playlist.remove_current() == "Midnight Bus", "Not yet: the current song is Midnight Bus."
    assert playlist.titles() == ["Lemon Skies", "Slow Rain"], "Not yet: removing the first song moves the head to Lemon Skies."
    assert playlist.current == "Lemon Skies", "Not yet: Lemon Skies plays next."
    playlist.next()
    assert playlist.remove_current() == "Slow Rain", "Not yet: the current song is Slow Rain."
    assert playlist.titles_backward() == ["Lemon Skies"], "Not yet: removing the last song moves the tail back to Lemon Skies."
    assert playlist.current == "Lemon Skies", "Not yet: with no song after it, the song before plays."


def test_remove_the_only_song_and_then_nothing():
    playlist = make("Midnight Bus")
    assert playlist.remove_current() == "Midnight Bus", "Not yet: the only song is the current one."
    assert playlist.titles() == [], "Not yet: the playlist is empty now."
    assert playlist.titles_backward() == [], "Not yet: an empty playlist has no tail either."
    assert playlist.current is None, "Not yet: nothing is playing on an empty playlist."
    assert playlist.remove_current() is None, "Not yet: an empty playlist has nothing to remove."


def test_a_long_up_next_queue_stays_fast():
    playlist = Playlist()
    for i in range(100000):
        playlist.add(f"Song {i}")
    started = time.perf_counter()

    def too_slow(i):
        return i % 1000 == 0 and time.perf_counter() - started > 2

    for i in range(50000):
        playlist.next()
    for i in range(100000):
        playlist.add_after_current(f"Next {i}")
        if too_slow(i):
            break
    forward = 0
    while playlist.next():
        forward += 1
        if too_slow(forward):
            break
    back = 0
    while playlist.previous():
        back += 1
        if too_slow(back):
            break
    for i in range(100000):
        playlist.remove_current()
        if too_slow(i):
            break
    seconds = time.perf_counter() - started
    assert seconds < 2, f"Not yet: the long up-next queue ran past 2 seconds: it was stopped at {seconds:.1f} seconds. When next, previous, play next and remove each change a few links, the whole run takes well under one second."
    assert forward == 149999, "Not yet: after 100,000 songs go in after Song 50000, next steps 149,999 times to the last song."
    assert back == 199999, "Not yet: previous steps 199,999 times, back to the first song."
    assert playlist.count == 100000, "Not yet: 100,000 of the 200,000 songs were removed."
    assert playlist.current == "Next 50000", "Not yet: after the first 100,000 songs are removed from the front, Next 50000 plays."
