// A doubly linked playlist with a "now playing" song. add, titles and
// titlesBackward are done; they show how the nodes link up. Your four
// methods are next, previous, addAfterCurrent and removeCurrent. The
// comment above each one says what it should do, with examples. Replace
// each body with your own code, and write its Big-O in its comment.

/**
 * One song in the playlist: its title, and references to the songs on
 * either side. prev is null for the first song; next is null for the last.
 */
export class SongNode {
  constructor(title) {
    this.title = title;
    this.prev = null;
    this.next = null;
  }
}

export class Playlist {
  #head = null;
  #tail = null;
  #current = null;
  #count = 0;

  /** How many songs the playlist holds. */
  get count() {
    return this.#count;
  }

  /** The title of the song now playing, or null on an empty playlist. */
  get current() {
    return this.#current === null ? null : this.#current.title;
  }

  /** Adds a song at the end. The first song added becomes the current one. */
  add(title) {
    const node = new SongNode(title);
    if (this.#tail === null) {
      this.#head = node;
      this.#current = node;
    } else {
      this.#tail.next = node;
      node.prev = this.#tail;
    }
    this.#tail = node;
    this.#count++;
  }

  /** Every title, walking forward from the head along next. */
  titles() {
    const titles = [];
    for (let node = this.#head; node !== null; node = node.next) {
      titles.push(node.title);
    }
    return titles;
  }

  /** Every title, walking backward from the tail along prev. */
  titlesBackward() {
    const titles = [];
    for (let node = this.#tail; node !== null; node = node.prev) {
      titles.push(node.title);
    }
    return titles;
  }

  /*
   * Move to the song after the current one and return true. If the
   * current song is the last one, or the playlist is empty, stay where
   * you are and return false.
   *
   * Midnight Bus (current), Lemon Skies: next() -> true, current is Lemon Skies
   * then next() -> false, current is still Lemon Skies
   *
   * Big-O: (write it here)
   */
  next() {
    return false;
  }

  /*
   * Move to the song before the current one and return true. If the
   * current song is the first one, or the playlist is empty, stay where
   * you are and return false.
   *
   * Midnight Bus, Lemon Skies (current): previous() -> true, current is Midnight Bus
   *
   * Big-O: (write it here)
   */
  previous() {
    return false;
  }

  /*
   * Put a new song right after the current one: "play this next". The
   * current song stays the same. Link the new node both ways, to the
   * current song and to the song that was after it. If the current song
   * was the tail, the new song is the tail now. On an empty playlist, the
   * new song is the only song, and it becomes the current one.
   *
   * Midnight Bus (current), Slow Rain: addAfterCurrent("Lemon Skies")
   * -> Midnight Bus, Lemon Skies, Slow Rain; current is still Midnight Bus
   *
   * Big-O: (write it here)
   */
  addAfterCurrent(title) {
    // Your code here.
  }

  /*
   * Take the current song out of the playlist and return its title. Link
   * the songs on either side of it to each other. The song after it
   * becomes the current one; if it was the last song, the song before it
   * does. Keep the head and the tail right. On an empty playlist, return
   * null.
   *
   * Midnight Bus, Lemon Skies (current), Slow Rain: removeCurrent() -> "Lemon Skies"
   * -> Midnight Bus, Slow Rain; current is Slow Rain
   *
   * Big-O: (write it here)
   */
  removeCurrent() {
    return null;
  }
}
