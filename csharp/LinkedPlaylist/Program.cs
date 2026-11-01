using LinkedPlaylist;

// Tries your playlist. Run it with: dotnet run --project LinkedPlaylist
Playlist playlist = new Playlist();
foreach (string song in new string[] { "Midnight Bus", "Lemon Skies", "Slow Rain", "Paper Boats" })
{
    playlist.Add(song);
}
Console.WriteLine("Playlist: " + string.Join(", ", playlist.Titles()));
Console.WriteLine("Now playing: " + playlist.Current);
playlist.Next();
Console.WriteLine("Next: " + playlist.Current);
playlist.Next();
Console.WriteLine("Next: " + playlist.Current);
playlist.Previous();
Console.WriteLine("Previous: " + playlist.Current);
playlist.AddAfterCurrent("Golden Hour");
Console.WriteLine("Added after " + playlist.Current + ": Golden Hour");
string? removed = playlist.RemoveCurrent();
Console.WriteLine("Removed: " + removed + ", now playing: " + playlist.Current);
Console.WriteLine("Playlist: " + string.Join(", ", playlist.Titles()));
Console.WriteLine("Backward: " + string.Join(", ", playlist.TitlesBackward()));
