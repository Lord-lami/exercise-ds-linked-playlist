# Tries your playlist. Run it with: python demo.py
from linked_playlist import Playlist

playlist = Playlist()
for song in ["Midnight Bus", "Lemon Skies", "Slow Rain", "Paper Boats"]:
    playlist.add(song)
print("Playlist: " + ", ".join(playlist.titles()))
print(f"Now playing: {playlist.current}")
playlist.next()
print(f"Next: {playlist.current}")
playlist.next()
print(f"Next: {playlist.current}")
playlist.previous()
print(f"Previous: {playlist.current}")
playlist.add_after_current("Golden Hour")
print(f"Added after {playlist.current}: Golden Hour")
removed = playlist.remove_current()
print(f"Removed: {removed}, now playing: {playlist.current}")
print("Playlist: " + ", ".join(playlist.titles()))
print("Backward: " + ", ".join(playlist.titles_backward()))
