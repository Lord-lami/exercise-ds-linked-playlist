// The tests for your playlist. You don't need to change this file.
// Each message says what was expected, so a failing test tells you what to look at.
import assert from "node:assert/strict";
import { test } from "node:test";
import { Playlist } from "./linked-playlist.js";

function make(...songs) {
  const playlist = new Playlist();
  for (const song of songs) {
    playlist.add(song);
  }
  return playlist;
}

test("next moves forward and stops at the end", () => {
  const playlist = make("Midnight Bus", "Lemon Skies", "Slow Rain");
  assert.equal(playlist.next(), true, "Not yet: there's a song after Midnight Bus, so next returns true.");
  assert.equal(playlist.current, "Lemon Skies", "Not yet: next moves from Midnight Bus to Lemon Skies.");
  assert.equal(playlist.next(), true, "Not yet: there's a song after Lemon Skies.");
  assert.equal(playlist.current, "Slow Rain", "Not yet: next moves on to Slow Rain.");
  assert.equal(playlist.next(), false, "Not yet: Slow Rain is the last song, so next returns false.");
  assert.equal(playlist.current, "Slow Rain", "Not yet: at the last song, next stays where it is.");
  assert.equal(new Playlist().next(), false, "Not yet: an empty playlist has nowhere to go.");
});

test("previous moves back and stops at the start", () => {
  const playlist = make("Midnight Bus", "Lemon Skies", "Slow Rain");
  playlist.next();
  playlist.next();
  assert.equal(playlist.previous(), true, "Not yet: there's a song before Slow Rain, so previous returns true.");
  assert.equal(playlist.current, "Lemon Skies", "Not yet: previous moves from Slow Rain back to Lemon Skies.");
  assert.equal(playlist.previous(), true, "Not yet: there's a song before Lemon Skies.");
  assert.equal(playlist.current, "Midnight Bus", "Not yet: previous moves back to Midnight Bus.");
  assert.equal(playlist.previous(), false, "Not yet: Midnight Bus is the first song, so previous returns false.");
  assert.equal(playlist.current, "Midnight Bus", "Not yet: at the first song, previous stays where it is.");
  assert.equal(new Playlist().previous(), false, "Not yet: an empty playlist has nowhere to go.");
});

test("addAfterCurrent in the middle", () => {
  const playlist = make("Midnight Bus", "Lemon Skies", "Slow Rain");
  playlist.addAfterCurrent("Golden Hour");
  assert.deepEqual(playlist.titles(), ["Midnight Bus", "Golden Hour", "Lemon Skies", "Slow Rain"],
    "Not yet: Golden Hour goes right after Midnight Bus, the current song.");
  assert.deepEqual(playlist.titlesBackward(), ["Slow Rain", "Lemon Skies", "Golden Hour", "Midnight Bus"],
    "Not yet: walking backward, Lemon Skies' prev must now be Golden Hour, and Golden Hour's prev Midnight Bus.");
  assert.equal(playlist.current, "Midnight Bus", "Not yet: adding after the current song doesn't change it.");
  assert.equal(playlist.count, 4, "Not yet: the playlist has 4 songs now.");
});

test("addAfterCurrent at the end moves the tail", () => {
  const playlist = make("Midnight Bus", "Lemon Skies");
  playlist.next();
  playlist.addAfterCurrent("Golden Hour");
  assert.deepEqual(playlist.titles(), ["Midnight Bus", "Lemon Skies", "Golden Hour"],
    "Not yet: Golden Hour goes after Lemon Skies, at the end.");
  assert.deepEqual(playlist.titlesBackward(), ["Golden Hour", "Lemon Skies", "Midnight Bus"],
    "Not yet: Golden Hour is the last song now, so the tail must point at it.");
  assert.equal(playlist.next(), true, "Not yet: there's a song after Lemon Skies now.");
  assert.equal(playlist.current, "Golden Hour", "Not yet: next from Lemon Skies reaches Golden Hour.");
});

test("addAfterCurrent on an empty playlist", () => {
  const playlist = new Playlist();
  playlist.addAfterCurrent("Golden Hour");
  assert.deepEqual(playlist.titles(), ["Golden Hour"], "Not yet: on an empty playlist, the new song is the only song.");
  assert.deepEqual(playlist.titlesBackward(), ["Golden Hour"], "Not yet: the only song is the head and the tail.");
  assert.equal(playlist.current, "Golden Hour", "Not yet: the only song becomes the current one.");
});

test("removeCurrent in the middle", () => {
  const playlist = make("Midnight Bus", "Lemon Skies", "Slow Rain");
  playlist.next();
  assert.equal(playlist.removeCurrent(), "Lemon Skies", "Not yet: removeCurrent returns the title it took out.");
  assert.deepEqual(playlist.titles(), ["Midnight Bus", "Slow Rain"], "Not yet: Midnight Bus' next must skip to Slow Rain.");
  assert.deepEqual(playlist.titlesBackward(), ["Slow Rain", "Midnight Bus"], "Not yet: Slow Rain's prev must skip back to Midnight Bus.");
  assert.equal(playlist.current, "Slow Rain", "Not yet: the song after the removed one plays next.");
  assert.equal(playlist.count, 2, "Not yet: 2 songs are left.");
});

test("removeCurrent at the front and the end", () => {
  const playlist = make("Midnight Bus", "Lemon Skies", "Slow Rain");
  assert.equal(playlist.removeCurrent(), "Midnight Bus", "Not yet: the current song is Midnight Bus.");
  assert.deepEqual(playlist.titles(), ["Lemon Skies", "Slow Rain"], "Not yet: removing the first song moves the head to Lemon Skies.");
  assert.equal(playlist.current, "Lemon Skies", "Not yet: Lemon Skies plays next.");
  playlist.next();
  assert.equal(playlist.removeCurrent(), "Slow Rain", "Not yet: the current song is Slow Rain.");
  assert.deepEqual(playlist.titlesBackward(), ["Lemon Skies"], "Not yet: removing the last song moves the tail back to Lemon Skies.");
  assert.equal(playlist.current, "Lemon Skies", "Not yet: with no song after it, the song before plays.");
});

test("remove the only song, and then nothing", () => {
  const playlist = make("Midnight Bus");
  assert.equal(playlist.removeCurrent(), "Midnight Bus", "Not yet: the only song is the current one.");
  assert.deepEqual(playlist.titles(), [], "Not yet: the playlist is empty now.");
  assert.deepEqual(playlist.titlesBackward(), [], "Not yet: an empty playlist has no tail either.");
  assert.equal(playlist.current, null, "Not yet: nothing is playing on an empty playlist.");
  assert.equal(playlist.removeCurrent(), null, "Not yet: an empty playlist has nothing to remove.");
});

test("a thousand songs, both ways", () => {
  const playlist = new Playlist();
  for (let i = 0; i < 1000; i++) {
    playlist.add(`Song ${i}`);
  }
  let moves = 0;
  while (playlist.next()) {
    moves++;
  }
  assert.equal(moves, 999, "Not yet: next moves 999 times through 1,000 songs.");
  while (playlist.previous()) {
    moves--;
  }
  assert.equal(moves, 0, "Not yet: previous walks all the way back.");
  while (playlist.removeCurrent() !== null) {
    // keep removing
  }
  assert.equal(playlist.count, 0, "Not yet: removing the current song again and again empties the playlist.");
});
