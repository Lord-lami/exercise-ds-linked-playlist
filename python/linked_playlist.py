# A doubly linked playlist with a "now playing" song. add, titles and
# titles_backward are done; they show how the nodes link up. Your four
# methods are next, previous, add_after_current and remove_current. The
# comment inside each one says what it should do, with examples. Replace
# each body with your own code, and write its Big-O in its comment.


class SongNode:
    """One song in the playlist: its title, and references to the songs on
    either side. prev is None for the first song; next is None for the last."""

    def __init__(self, title):
        self.title = title
        self.prev = None
        self.next = None


class Playlist:
    def __init__(self):
        self._head = None
        self._tail = None
        self._current = None
        self._count = 0

    @property
    def count(self):
        """How many songs the playlist holds."""
        return self._count

    @property
    def current(self):
        """The title of the song now playing, or None on an empty playlist."""
        return None if self._current is None else self._current.title

    def add(self, title):
        """Adds a song at the end. The first song added becomes the current one."""
        node = SongNode(title)
        if self._tail is None:
            self._head = node
            self._current = node
        else:
            self._tail.next = node
            node.prev = self._tail
        self._tail = node
        self._count += 1

    def titles(self):
        """Every title, walking forward from the head along next."""
        titles = []
        node = self._head
        while node is not None:
            titles.append(node.title)
            node = node.next
        return titles

    def titles_backward(self):
        """Every title, walking backward from the tail along prev."""
        titles = []
        node = self._tail
        while node is not None:
            titles.append(node.title)
            node = node.prev
        return titles

    def next(self):
        """
        Move to the song after the current one and return True. If the
        current song is the last one, or the playlist is empty, stay where
        you are and return False.

        Midnight Bus (current), Lemon Skies: next() -> True, current is Lemon Skies
        then next() -> False, current is still Lemon Skies

        Big-O: (write it here)
        """
        return False

    def previous(self):
        """
        Move to the song before the current one and return True. If the
        current song is the first one, or the playlist is empty, stay where
        you are and return False.

        Midnight Bus, Lemon Skies (current): previous() -> True, current is Midnight Bus

        Big-O: (write it here)
        """
        return False

    def add_after_current(self, title):
        """
        Put a new song right after the current one: "play this next". The
        current song stays the same. Link the new node both ways, to the
        current song and to the song that was after it. If the current song
        was the tail, the new song is the tail now. On an empty playlist, the
        new song is the only song, and it becomes the current one.

        Midnight Bus (current), Slow Rain: add_after_current("Lemon Skies")
        -> Midnight Bus, Lemon Skies, Slow Rain; current is still Midnight Bus

        Big-O: (write it here)
        """
        pass  # your code here

    def remove_current(self):
        """
        Take the current song out of the playlist and return its title. Link
        the songs on either side of it to each other. The song after it
        becomes the current one; if it was the last song, the song before it
        does. Keep the head and the tail right. On an empty playlist, return
        None.

        Midnight Bus, Lemon Skies (current), Slow Rain: remove_current() -> "Lemon Skies"
        -> Midnight Bus, Slow Rain; current is Slow Rain

        Big-O: (write it here)
        """
        return None
