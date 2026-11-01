# Exercise: A Playlist You Can Walk

Exercise code for the lesson **Exercise: A Playlist You Can Walk** in the course *Data Structures and Big-O, Demystified* on [ask-codey.com](https://www.ask-codey.com).

A music player has a "now playing" song, previous and next buttons, a "play next" button and a "remove" button. You'll build it as a doubly linked list: every song is a node that knows the song before it and the song after it. The `Playlist` class can already add a song at the end and list its titles forward and backward (read those methods: they show how the nodes link up). There are four methods to finish:

- `next` (`Next` in C#): move to the song after the current one
- `previous` (`Previous`): move to the song before it
- `addAfterCurrent` (`AddAfterCurrent`, `add_after_current` in Python): put a new song right after the current one
- `removeCurrent` (`RemoveCurrent`, `remove_current`): take the current song out and return its title

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

## Tips

- **Start with `next` and `previous`.** They're one step each along a reference, and the other tests use them to get into position.
- **Draw it before you code it.** Three boxes with arrows both ways, on paper: which arrows change when a song goes in after the middle one? When it comes out?
- **Every link goes both ways.** When you change a node's `next`, ask which node's `prev` has to change to match. The tests walk the playlist backward too, so a missing `prev` shows up.
- **Check the ends.** Adding after the last song moves the tail; removing the first or last song moves the head or the tail; removing the only song empties everything.

## Hand it in

When it works, push your solution to a public repo of your own on GitHub, then paste that repo's address into **Submit your exercise** on the lesson page at [ask-codey.com](https://www.ask-codey.com). That's what completes the lesson. The lesson page shows the Git commands, step by step.
