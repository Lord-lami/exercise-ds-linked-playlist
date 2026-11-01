// Tries your playlist. Run it with: java src/main/java/Demo.java
public class Demo {
    public static void main(String[] args) {
        Playlist playlist = new Playlist();
        for (String song : new String[] {"Midnight Bus", "Lemon Skies", "Slow Rain", "Paper Boats"}) {
            playlist.add(song);
        }
        System.out.println("Playlist: " + String.join(", ", playlist.titles()));
        System.out.println("Now playing: " + playlist.current());
        playlist.next();
        System.out.println("Next: " + playlist.current());
        playlist.next();
        System.out.println("Next: " + playlist.current());
        playlist.previous();
        System.out.println("Previous: " + playlist.current());
        playlist.addAfterCurrent("Golden Hour");
        System.out.println("Added after " + playlist.current() + ": Golden Hour");
        String removed = playlist.removeCurrent();
        System.out.println("Removed: " + removed + ", now playing: " + playlist.current());
        System.out.println("Playlist: " + String.join(", ", playlist.titles()));
        System.out.println("Backward: " + String.join(", ", playlist.titlesBackward()));
    }
}
