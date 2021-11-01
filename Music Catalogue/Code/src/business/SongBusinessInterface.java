package business;

import java.util.List;

import beans.Song;

//Interface for Song Business 
public interface SongBusinessInterface {
	
	public void addSong(Song song);
	public void test();
	public List<Song> getSongs();
	public void setSongs(List<Song> songs);
	public void changeSong(Song song);
	public List<Song> getFewSongs();
	public void deleteSong(Song song);
	public List<Song> getSearchedSongs(Song song);
	public List<Song> getRandomizedSongs(Song song);
	


}
