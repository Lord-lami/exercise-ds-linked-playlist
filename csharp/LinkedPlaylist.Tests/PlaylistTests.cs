using System.Diagnostics;
using LinkedPlaylist;

namespace LinkedPlaylist.Tests;

// The tests for your playlist. You don't need to change this file.
// Each message says what was expected, so a failing test tells you what to look at.

[TestClass]
public class PlaylistTests
{
    private static Playlist Make(params string[] songs)
    {
        Playlist playlist = new Playlist();
        foreach (string song in songs)
        {
            playlist.Add(song);
        }
        return playlist;
    }

    private static List<string> L(params string[] songs) => new List<string>(songs);

    [TestMethod]
    public void NextMovesForwardAndStopsAtTheEnd()
    {
        Playlist playlist = Make("Midnight Bus", "Lemon Skies", "Slow Rain");
        Assert.IsTrue(playlist.Next(), "Not yet: there's a song after Midnight Bus, so Next returns true.");
        Assert.AreEqual("Lemon Skies", playlist.Current, "Not yet: Next moves from Midnight Bus to Lemon Skies.");
        Assert.IsTrue(playlist.Next(), "Not yet: there's a song after Lemon Skies.");
        Assert.AreEqual("Slow Rain", playlist.Current, "Not yet: Next moves on to Slow Rain.");
        Assert.IsFalse(playlist.Next(), "Not yet: Slow Rain is the last song, so Next returns false.");
        Assert.AreEqual("Slow Rain", playlist.Current, "Not yet: at the last song, Next stays where it is.");
        Assert.IsFalse(new Playlist().Next(), "Not yet: an empty playlist has nowhere to go.");
    }

    [TestMethod]
    public void PreviousMovesBackAndStopsAtTheStart()
    {
        Playlist playlist = Make("Midnight Bus", "Lemon Skies", "Slow Rain");
        playlist.Next();
        playlist.Next();
        Assert.IsTrue(playlist.Previous(), "Not yet: there's a song before Slow Rain, so Previous returns true.");
        Assert.AreEqual("Lemon Skies", playlist.Current, "Not yet: Previous moves from Slow Rain back to Lemon Skies.");
        Assert.IsTrue(playlist.Previous(), "Not yet: there's a song before Lemon Skies.");
        Assert.AreEqual("Midnight Bus", playlist.Current, "Not yet: Previous moves back to Midnight Bus.");
        Assert.IsFalse(playlist.Previous(), "Not yet: Midnight Bus is the first song, so Previous returns false.");
        Assert.AreEqual("Midnight Bus", playlist.Current, "Not yet: at the first song, Previous stays where it is.");
        Assert.IsFalse(new Playlist().Previous(), "Not yet: an empty playlist has nowhere to go.");
    }

    [TestMethod]
    public void AddAfterCurrentInTheMiddle()
    {
        Playlist playlist = Make("Midnight Bus", "Lemon Skies", "Slow Rain");
        playlist.AddAfterCurrent("Golden Hour");
        CollectionAssert.AreEqual(L("Midnight Bus", "Golden Hour", "Lemon Skies", "Slow Rain"), playlist.Titles(),
            "Not yet: Golden Hour goes right after Midnight Bus, the current song.");
        CollectionAssert.AreEqual(L("Slow Rain", "Lemon Skies", "Golden Hour", "Midnight Bus"), playlist.TitlesBackward(),
            "Not yet: walking backward, Lemon Skies' Prev must now be Golden Hour, and Golden Hour's Prev Midnight Bus.");
        Assert.AreEqual("Midnight Bus", playlist.Current, "Not yet: adding after the current song doesn't change it.");
        Assert.AreEqual(4, playlist.Count, "Not yet: the playlist has 4 songs now.");
    }

    [TestMethod]
    public void AddAfterCurrentAtTheEndMovesTheTail()
    {
        Playlist playlist = Make("Midnight Bus", "Lemon Skies");
        playlist.Next();
        playlist.AddAfterCurrent("Golden Hour");
        CollectionAssert.AreEqual(L("Midnight Bus", "Lemon Skies", "Golden Hour"), playlist.Titles(),
            "Not yet: Golden Hour goes after Lemon Skies, at the end.");
        CollectionAssert.AreEqual(L("Golden Hour", "Lemon Skies", "Midnight Bus"), playlist.TitlesBackward(),
            "Not yet: Golden Hour is the last song now, so the tail must point at it.");
        Assert.IsTrue(playlist.Next(), "Not yet: there's a song after Lemon Skies now.");
        Assert.AreEqual("Golden Hour", playlist.Current, "Not yet: Next from Lemon Skies reaches Golden Hour.");
    }

    [TestMethod]
    public void AddAfterCurrentOnAnEmptyPlaylist()
    {
        Playlist playlist = new Playlist();
        playlist.AddAfterCurrent("Golden Hour");
        CollectionAssert.AreEqual(L("Golden Hour"), playlist.Titles(), "Not yet: on an empty playlist, the new song is the only song.");
        CollectionAssert.AreEqual(L("Golden Hour"), playlist.TitlesBackward(), "Not yet: the only song is the head and the tail.");
        Assert.AreEqual("Golden Hour", playlist.Current, "Not yet: the only song becomes the current one.");
    }

    [TestMethod]
    public void RemoveCurrentInTheMiddle()
    {
        Playlist playlist = Make("Midnight Bus", "Lemon Skies", "Slow Rain");
        playlist.Next();
        Assert.AreEqual("Lemon Skies", playlist.RemoveCurrent(), "Not yet: RemoveCurrent returns the title it took out.");
        CollectionAssert.AreEqual(L("Midnight Bus", "Slow Rain"), playlist.Titles(), "Not yet: Midnight Bus' Next must skip to Slow Rain.");
        CollectionAssert.AreEqual(L("Slow Rain", "Midnight Bus"), playlist.TitlesBackward(), "Not yet: Slow Rain's Prev must skip back to Midnight Bus.");
        Assert.AreEqual("Slow Rain", playlist.Current, "Not yet: the song after the removed one plays next.");
        Assert.AreEqual(2, playlist.Count, "Not yet: 2 songs are left.");
    }

    [TestMethod]
    public void RemoveCurrentAtTheFrontAndTheEnd()
    {
        Playlist playlist = Make("Midnight Bus", "Lemon Skies", "Slow Rain");
        Assert.AreEqual("Midnight Bus", playlist.RemoveCurrent(), "Not yet: the current song is Midnight Bus.");
        CollectionAssert.AreEqual(L("Lemon Skies", "Slow Rain"), playlist.Titles(), "Not yet: removing the first song moves the head to Lemon Skies.");
        Assert.AreEqual("Lemon Skies", playlist.Current, "Not yet: Lemon Skies plays next.");
        playlist.Next();
        Assert.AreEqual("Slow Rain", playlist.RemoveCurrent(), "Not yet: the current song is Slow Rain.");
        CollectionAssert.AreEqual(L("Lemon Skies"), playlist.TitlesBackward(), "Not yet: removing the last song moves the tail back to Lemon Skies.");
        Assert.AreEqual("Lemon Skies", playlist.Current, "Not yet: with no song after it, the song before plays.");
    }

    [TestMethod]
    public void RemoveTheOnlySongAndThenNothing()
    {
        Playlist playlist = Make("Midnight Bus");
        Assert.AreEqual("Midnight Bus", playlist.RemoveCurrent(), "Not yet: the only song is the current one.");
        Assert.AreEqual(0, playlist.Titles().Count, "Not yet: the playlist is empty now.");
        Assert.AreEqual(0, playlist.TitlesBackward().Count, "Not yet: an empty playlist has no tail either.");
        Assert.IsNull(playlist.Current, "Not yet: nothing is playing on an empty playlist.");
        Assert.IsNull(playlist.RemoveCurrent(), "Not yet: an empty playlist has nothing to remove.");
    }

    [TestMethod]
    public void ALongUpNextQueueStaysFast()
    {
        Playlist playlist = new Playlist();
        for (int i = 0; i < 100000; i++)
        {
            playlist.Add("Song " + i);
        }
        Stopwatch watch = Stopwatch.StartNew();
        bool TooSlow(int i) => i % 1000 == 0 && watch.Elapsed.TotalSeconds > 2;

        for (int i = 0; i < 50000; i++)
        {
            playlist.Next();
        }
        for (int i = 0; i < 100000; i++)
        {
            playlist.AddAfterCurrent("Next " + i);
            if (TooSlow(i))
            {
                break;
            }
        }
        int forward = 0;
        while (playlist.Next())
        {
            forward++;
            if (TooSlow(forward))
            {
                break;
            }
        }
        int back = 0;
        while (playlist.Previous())
        {
            back++;
            if (TooSlow(back))
            {
                break;
            }
        }
        for (int i = 0; i < 100000; i++)
        {
            playlist.RemoveCurrent();
            if (TooSlow(i))
            {
                break;
            }
        }
        watch.Stop();
        Assert.IsTrue(watch.Elapsed.TotalSeconds < 2, "Not yet: the long up-next queue ran past 2 seconds: it was stopped at " + watch.Elapsed.TotalSeconds.ToString("0.0") + " seconds. When next, previous, play next and remove each change a few links, the whole run takes well under one second.");
        Assert.AreEqual(149999, forward, "Not yet: after 100,000 songs go in after Song 50000, next steps 149,999 times to the last song.");
        Assert.AreEqual(199999, back, "Not yet: previous steps 199,999 times, back to the first song.");
        Assert.AreEqual(100000, playlist.Count, "Not yet: 100,000 of the 200,000 songs were removed.");
        Assert.AreEqual("Next 50000", playlist.Current, "Not yet: after the first 100,000 songs are removed from the front, Next 50000 plays.");
    }
}
