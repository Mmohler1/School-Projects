package data;

import java.util.List;


import beans.Song;

//Interface for the Database Service
public interface DataSongInterface {
	public List<Song> findAll();
    public List<Song> findFew();
	public void create(Song song);
	public void update(Song song);
	public void delete(int id);
	public Song findById(int id);
	public void test();
	public List<Song> search(Song song);
	public List<Song> byGenre(Song song);


}
