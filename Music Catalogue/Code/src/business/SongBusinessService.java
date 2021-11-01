package business;

import beans.Song;
import data.DataSongInterface;

import java.util.ArrayList;
import java.util.List;
import java.util.Random;

import javax.ejb.EJB;
import javax.ejb.Local;
import javax.ejb.LocalBean;
import javax.ejb.Stateless;
import javax.enterprise.inject.Alternative;
import javax.faces.bean.ViewScoped;

/**
 * Session Bean implementation class SongBusinessService
 */
@Stateless
@Local(SongBusinessInterface.class)
@LocalBean
@Alternative
@ViewScoped
public class SongBusinessService implements SongBusinessInterface {
	
	
	@EJB
	DataSongInterface service;

    /**
     * Default constructor. 
     */
    public SongBusinessService() {
        // TODO Auto-generated constructor stub
    }

    
    
    
	/**
     * @see SongBusinessInterface#addSong(Song)
     * Adds a song by calling the database.
     * @Param Song
     */
    public void addSong(Song song) {
    	
        // TODO Auto-generated method stub
    	service.create(song);
    	
    }

	/**
     * @see SongBusinessInterface#test()
     * Simple print line to show interface is working
     */
    public void test() {
        // TODO Auto-generated method stub
    	System.out.println("Song Business service worked!");
    }
    
    
    
	/**
     *Updates song found in id, if null output message that no song was found..
     *
     *@Param Song
     */
    public void changeSong(Song song) {
    	
    	//Find out if the location even has a song.
    	if (service.findById(song.getId()) == null)
    	{
    		System.out.println("No Song Found!");
    	}
    	else
    	service.update(song);
    }
    
    
	/**
     *Delete song
     *
     *@Param Song
     */
    public void deleteSong(Song song) {
    	service.delete(song.getId());
    }
    
	//Getters and Setters for song list
	public List<Song> getSongs() {
		return service.findAll();
	}


	public void setSongs(List<Song> songs) {
		//this.songs = songs;
	}

	//Getters from detailed list
	public List<Song> getFewSongs() {
		return service.findFew();
	}
	
	//Getters from search list
	public List<Song> getSearchedSongs(Song song) {
		System.out.println(song.getGenre() + " "+ song.getNum());
		return service.search(song);
	}
	
	
	/**
     *Gets random songs for the user based on genre
     *
     *@Param Song
     */
	public List<Song> getRandomizedSongs(Song song)
	{
		//List of songs by genre and new list for random songs
		List<Song> genreList = service.byGenre(song);
		List<Song> randomList = new ArrayList<Song>();
		Random rand = new Random(); //For picking a random song
		
		
		//If the size of the list is 0 then don't continue
		if (genreList.size() > 0)
		{
			int[] randomNum = new int[genreList.size()];
			int randomNumber;
			
			int count = 0;
			//While loop that adds to the list ass long is count is less the number of songs the user wanted
			//AND as long as there are that many songs in the list.
			while ((count < song.getNum()) && (count < genreList.size()))
			{
				randomNumber = rand.nextInt(genreList.size());
				int count2 = 0;
				
				//Prevents the same song from being added twice
				while(count2 < count)
				{
					
					
					if (randomNum[count2] == randomNumber)
					{
						randomNumber = rand.nextInt(genreList.size());
						count2 = 0;
					}
					else
					{
						count2++; //Only add to the counter if a new number is found
					}
				}
				
				randomNum[count] = randomNumber;
				randomList.add(genreList.get(randomNumber)); //Need to make a list of numbers not to repeat
				count++;
			}
			
		}
		
		
		return randomList;
		
	}
}
