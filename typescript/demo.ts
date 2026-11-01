// Tries your playlist. Run it with: node demo.ts
import { Playlist } from "./linked-playlist.ts";

const playlist = new Playlist();
for (const song of ["Midnight Bus", "Lemon Skies", "Slow Rain", "Paper Boats"]) {
  playlist.add(song);
}
console.log(`Playlist: ${playlist.titles().join(", ")}`);
console.log(`Now playing: ${playlist.current}`);
playlist.next();
console.log(`Next: ${playlist.current}`);
playlist.next();
console.log(`Next: ${playlist.current}`);
playlist.previous();
console.log(`Previous: ${playlist.current}`);
playlist.addAfterCurrent("Golden Hour");
console.log(`Added after ${playlist.current}: Golden Hour`);
const removed = playlist.removeCurrent();
console.log(`Removed: ${removed}, now playing: ${playlist.current}`);
console.log(`Playlist: ${playlist.titles().join(", ")}`);
console.log(`Backward: ${playlist.titlesBackward().join(", ")}`);
