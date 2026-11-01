namespace LinkedPlaylist;

// One song in the playlist: its title, and references to the songs on
// either side. Prev is null for the first song; Next is null for the last.
public class SongNode
{
    public string Title;
    public SongNode? Prev;
    public SongNode? Next;

    public SongNode(string title)
    {
        Title = title;
    }
}

// A doubly linked playlist with a "now playing" song. Add, Titles and
// TitlesBackward are done; they show how the nodes link up. Your four
// methods are Next, Previous, AddAfterCurrent and RemoveCurrent. The
// comment above each one says what it should do, with examples. Replace
// each body with your own code, and write its Big-O in its comment.
public class Playlist
{
    private SongNode? head;
    private SongNode? tail;
    private SongNode? current;

    // How many songs the playlist holds.
    public int Count { get; private set; }

    // The title of the song now playing, or null on an empty playlist.
    public string? Current => current?.Title;

    // Adds a song at the end. The first song added becomes the current one.
    public void Add(string title)
    {
        SongNode node = new SongNode(title);
        if (tail == null)
        {
            head = node;
            current = node;
        }
        else
        {
            tail.Next = node;
            node.Prev = tail;
        }
        tail = node;
        Count++;
    }

    // Every title, walking forward from the head along Next.
    public List<string> Titles()
    {
        List<string> titles = new List<string>();
        for (SongNode? node = head; node != null; node = node.Next)
        {
            titles.Add(node.Title);
        }
        return titles;
    }

    // Every title, walking backward from the tail along Prev.
    public List<string> TitlesBackward()
    {
        List<string> titles = new List<string>();
        for (SongNode? node = tail; node != null; node = node.Prev)
        {
            titles.Add(node.Title);
        }
        return titles;
    }

    /*
     * Move to the song after the current one and return true. If the
     * current song is the last one, or the playlist is empty, stay where
     * you are and return false.
     *
     * Midnight Bus (current), Lemon Skies: Next() -> true, current is Lemon Skies
     * then Next() -> false, current is still Lemon Skies
     *
     * Big-O: (write it here)
     */
    public bool Next()
    {
        return false;
    }

    /*
     * Move to the song before the current one and return true. If the
     * current song is the first one, or the playlist is empty, stay where
     * you are and return false.
     *
     * Midnight Bus, Lemon Skies (current): Previous() -> true, current is Midnight Bus
     *
     * Big-O: (write it here)
     */
    public bool Previous()
    {
        return false;
    }

    /*
     * Put a new song right after the current one: "play this next". The
     * current song stays the same. Link the new node both ways, to the
     * current song and to the song that was after it. If the current song
     * was the tail, the new song is the tail now. On an empty playlist, the
     * new song is the only song, and it becomes the current one.
     *
     * Midnight Bus (current), Slow Rain: AddAfterCurrent("Lemon Skies")
     * -> Midnight Bus, Lemon Skies, Slow Rain; current is still Midnight Bus
     *
     * Big-O: (write it here)
     */
    public void AddAfterCurrent(string title)
    {
        // Your code here.
    }

    /*
     * Take the current song out of the playlist and return its title. Link
     * the songs on either side of it to each other. The song after it
     * becomes the current one; if it was the last song, the song before it
     * does. Keep the head and the tail right. On an empty playlist, return
     * null.
     *
     * Midnight Bus, Lemon Skies (current), Slow Rain: RemoveCurrent() -> "Lemon Skies"
     * -> Midnight Bus, Slow Rain; current is Slow Rain
     *
     * Big-O: (write it here)
     */
    public string? RemoveCurrent()
    {
        return null;
    }
}
