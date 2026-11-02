# Exercise: A Playlist You Can Walk

Exercise code for the lesson **Exercise: A Playlist You Can Walk** in the course *Data Structures and Big-O, Demystified* on [ask-codey.com](https://www.ask-codey.com).

## The ticket

> **From:** the player team, the music app
>
> Our up-next queue is a list, and "Play next" puts the song at its index after the current one. On long queues it lags: at 100,000 songs, every tap moves tens of thousands of songs to make room. The new playlist keeps a song "now playing", and:
>
> 1. **Next and Previous** move one song along, and stop at the ends.
> 2. **Play next** puts a song right after the one playing, and the one playing keeps playing.
> 3. **Remove** takes the song playing out of the playlist; the song after it plays next, or the one before it if it was the last.
> 4. **Every button takes the same moment** on a 4-song playlist or a 200,000-song one. Previous included: nobody waits while the app looks for the song before.

The code that's there has a `SongNode` for each song, with a title and a reference to the song on either side (`prev` and `next`), and a `Playlist` that keeps the first song, the last song and the one playing. It can already add a song at the end and list its titles forward and backward (read those methods: they show how the songs link up). Four methods are yours to finish:

- `next` (`Next` in C#): move to the song after the current one
- `previous` (`Previous`): move to the song before it
- `addAfterCurrent` (`AddAfterCurrent`, `add_after_current` in Python): "play next", a new song right after the current one
- `removeCurrent` (`RemoveCurrent`, `remove_current`): take the current song out and return its title

Nine tests check it, ending with a long up-next queue: 100,000 songs, 100,000 taps of Play next, a walk to the end and all the way back, and 100,000 removes, in under 2 seconds.

## Pick your language

The same exercise is here in five languages. Work in the folder for the language you set up; you only need one.

| Folder | Your code | The tests |
|---|---|---|
| `csharp/` | `LinkedPlaylist/Playlist.cs` | `LinkedPlaylist.Tests/PlaylistTests.cs` |
| `java/` | `src/main/java/Playlist.java` | `src/test/java/PlaylistTest.java` |
| `python/` | `linked_playlist.py` | `test_linked_playlist.py` |
| `javascript/` | `linked-playlist.js` | `linked-playlist.test.js` |
| `typescript/` | `linked-playlist.ts` | `linked-playlist.test.ts` |

Each method returns `false` or null (`None` in Python), or does nothing, for now. The comment above it says what it should do, with examples. Replace its body with your own code, and write its Big-O on its **Big-O:** line.

Every folder also has a small demo program that walks a four-song playlist. You don't need to change the tests or the demo.

## Run it and check your work

Open your language's folder in VS Code (**File > Open Folder**), then open its terminal (**Terminal > New Terminal**) and run these commands there.

**C#**

```bash
dotnet run --project LinkedPlaylist
dotnet test
```

**Java** (the Maven Wrapper downloads Maven the first time, so the first run takes a minute)

```bash
java src/main/java/Demo.java
./mvnw test
```

On Windows in PowerShell, write `.\mvnw test` instead of `./mvnw test`.

**Python** (once: make a virtual environment and install pytest into it)

```bash
python -m venv .venv
```

Then turn it on: `.venv\Scripts\Activate.ps1` in PowerShell on Windows, or `source .venv/bin/activate` on macOS (where the command is `python3`, not `python`). Then:

```bash
pip install -r requirements.txt
python demo.py
python -m pytest
```

**JavaScript**

```bash
node demo.js
npm test
```

**TypeScript** (once: `npm install`, which installs TypeScript)

```bash
node demo.ts
npm test
```

At first every test fails: that is expected. Each method you finish turns its tests green. A failing test's message starts with **Not yet:** and says what it expected.

<details>
<summary>Stuck?</summary>

- **Where to look:** the ticket asks for what *"Play Next" Without Moving Everyone: Linked Lists* and *Reaching the Last Song, and Going Back One: Doubly Linked Lists* build, a **doubly linked list**: each song knows the song before it and the song after it, so every button changes a few references and moves no song.
- **Start with `next` and `previous`.** They're one step each along a reference, and the other tests use them to get into position.
- **Draw it before you code it.** Three boxes with arrows both ways, on paper: which arrows change when a song goes in after the middle one? When it comes out?
- **Every link goes both ways.** When you change a node's `next`, ask which node's `prev` has to change to match. The tests walk the playlist backward too, so a missing `prev` shows up.
- **Check the ends.** Adding after the last song moves the tail; removing the first or last song moves the head or the tail; removing the only song empties everything.
- **The long-queue test stops early** once it passes 2 seconds. If yours stops, look for a loop inside a button: a `previous` that walks from the first song to find the one before is the usual one.

</details>

<details>
<summary>Why Should I Choose This?</summary>

Open this once your tests are green. The long-queue test taps Play next 100,000 times on a 100,000-song queue, then walks to the end and back.

- **Today's list, with an insert after the song playing:** every tap moves every song after it one slot along. With the song playing at position 50,000, that's 50,000 moves or more per tap, about 10 billion for the test's 100,000 taps. O(n) per tap.
- **Songs that only know the song after them:** Play next is quick, but Previous has to walk from the first song to find the one before. Walking back from song 200,000 that way is about 20 billion steps; on the test's clock it was stopped at 2 to 6 seconds in every language.
- **Songs that know both neighbors:** every button changes at most four references, O(1), and the whole test takes 0.04 seconds in C#, Java and JavaScript and 0.2 in Python.
- **What it gives up:** "jump to song 500" now means walking 500 songs from the front, O(n), and each song carries two references. When the job is mostly jumping by number, the growing array from the last chapter wins.

</details>

## Hand it in

When it works, push your solution to a public repo of your own on GitHub, then paste that repo's address into **Submit your exercise** on the lesson page at [ask-codey.com](https://www.ask-codey.com). That's what completes the lesson. The lesson page shows the Git commands, step by step.
