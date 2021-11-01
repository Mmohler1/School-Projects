package data;

import beans.Song;

import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.sql.Statement;
import java.util.ArrayList;
import java.util.List;
import javax.ejb.Local;
import javax.ejb.LocalBean;
import javax.ejb.Stateless;

/**
 * Session Bean implementation class SongsDataService
 */
@Stateless
@Local(DataSongInterface.class)
@LocalBean
public class SongsDataService implements DataSongInterface {

	//Sets up which server to access for the entire program. 
	String url = "jdbc:postgresql://localhost:5432/postgres";
	String username = "postgres";
	String password = "chair";        //NEEDS TO BE CHANGE FOR EACH PERSON'S POSTGRES PASSWORD!!!!!!!!!!!!

	
	
	/**
     * @see DataSongInterface#display()
     * Displays all the songs to the user from the database by returning it as a list of songs
     */
    public List<Song> findAll() {
        // TODO Auto-generated method stub
		Connection conn = null;	
		String sql = "SELECT * FROM milestone.SONGS";
		List<Song> songs = new ArrayList<Song>();
		
		try
		{
			//Connecting to database
			conn = DriverManager.getConnection(url, username, password);
			
			//Execute SQL Query and loop
			Statement stmt = conn.createStatement();
			ResultSet rs = stmt.executeQuery(sql);
			
			while(rs.next())
			{
				//add new order with each new table
				songs.add(new Song(rs.getInt("ID"),
						rs.getInt("SONG_NO"),
						rs.getString("SONG_NAME"), 
						rs.getString("SONG_ALBUM"), 
						rs.getString("SONG_ARTIST"),
						rs.getString("SONG_GENRE")));			
			}
			
			
			conn.close();
			return songs;
		}
		catch (SQLException e)
		{
			
			e.printStackTrace();
			return null;
			
		}
		finally
		{
			//Database cleaning
			if(conn != null)
			{
				try
				{
					conn.close();
				}
				catch (SQLException e)
				{
					e.printStackTrace();
				}
			}
			
		}
    }
	
    
	/**
     *Pulls the 4 recent songs to be display on the main screen
     *
     */
    public List<Song> findFew() {
        // TODO Auto-generated method stub
		Connection conn = null;	
		String sql = "SELECT * FROM milestone.SONGS ORDER BY ID DESC LIMIT 4";
		List<Song> songs = new ArrayList<Song>();
		
		try
		{
			//Connecting to database
			conn = DriverManager.getConnection(url, username, password);
			
			//Execute SQL Query and loop
			Statement stmt = conn.createStatement();
			ResultSet rs = stmt.executeQuery(sql);
			
			while(rs.next())
			{
				//add new order with each new table
				songs.add(new Song(rs.getInt("ID"),
						rs.getInt("SONG_NO"),
						rs.getString("SONG_NAME"), 
						rs.getString("SONG_ALBUM"), 
						rs.getString("SONG_ARTIST"),
						rs.getString("SONG_GENRE")));			
			}
			
			
			conn.close();
			return songs;
		}
		catch (SQLException e)
		{
			
			e.printStackTrace();
			return null;
			
		}
		finally
		{
			//Database cleaning
			if(conn != null)
			{
				try
				{
					conn.close();
				}
				catch (SQLException e)
				{
					e.printStackTrace();
				}
			}
			
		}
    }
    
    
    
    
    
    
	/**
     * @see DataSongInterface#update(Song)
     * Updates the songs based on the ID in the database.
     * @Param Song
     */
    public void update(Song song) {
        // TODO Auto-generated method stub
   	Connection conn = null;
    	
    	Song updateSong = song;
    	//Default values for string
		String sql = "UPDATE milestone.SONGS SET " + 
				"SONG_NO = "+ updateSong.getNum() + ", " +
				"SONG_NAME = '"+ updateSong.getName() + "', " +
				"SONG_ALBUM = '"+ updateSong.getAlbum() + "', " + 
				"SONG_ARTIST = '"+ updateSong.getArtist() + "', " +
				"SONG_GENRE = '"+updateSong.getGenre() + 
				"' WHERE ID = " + updateSong.getId() + ";";
		
		try
		{
			conn = DriverManager.getConnection(url, username, password);
			Statement stmt = conn.createStatement();
			stmt.executeUpdate(sql);
		}
		catch (SQLException e)
		{
			e.printStackTrace();
		}
		finally
		{
			//Database cleaning
			if(conn != null)
			{
				try
				{
					conn.close();
				}
				catch (SQLException e)
				{
					e.printStackTrace();
				}
			}
		}
    	
    }

    
	/**
     * 
     * Deletes the chosen song from database.
     * @Param int
     */
    public void delete(int id) {
        // TODO Auto-generated method stub
    	Connection conn = null;
    	
    	String sql = "DELETE FROM milestone.SONGS"+
    					" WHERE ID = "
    					+ id +";";
    	
    	try
		{
			conn = DriverManager.getConnection(url, username, password);
			Statement stmt = conn.createStatement();
			stmt.executeUpdate(sql);
		}
		catch (SQLException e)
		{
			e.printStackTrace();
		}
		finally
		{
			//Database cleaning
			if(conn != null)
			{
				try
				{
					conn.close();
				}
				catch (SQLException e)
				{
					e.printStackTrace();
				}
			}
		}
    }


	/**
     * Adds a song to the database
     * @Param Song
     */
    public void create(Song song) {
        // TODO Auto-generated method stub
    	Connection conn = null;
    	
    	Song newSong = song;
    	//Default values for string
		String sql = "INSERT INTO  milestone.SONGS(SONG_NO, SONG_NAME, SONG_ALBUM, SONG_ARTIST, SONG_GENRE) VALUES(" + 
				newSong.getNum() + ", '" +
				newSong.getName() + "', '" +
				newSong.getAlbum() + "', '" + 
				newSong.getArtist() + "', '" +
				newSong.getGenre() + "')";
		try
		{
			conn = DriverManager.getConnection(url, username, password);
			Statement stmt = conn.createStatement();
			stmt.executeUpdate(sql);
		}
		catch (SQLException e)
		{
			e.printStackTrace();
		}
		finally
		{
			//Database cleaning
			if(conn != null)
			{
				try
				{
					conn.close();
				}
				catch (SQLException e)
				{
					e.printStackTrace();
				}
			}
		}
    }
    
    
    
    
    
	/**
     * Returns song based on the ID in the Database. If ID is empty then return null.
     * @Param int
     */
    public Song findById(int id) {
        // TODO Auto-generated method stub
    	Connection conn = null;	
		String sql = "SELECT * FROM milestone.SONGS";
		Song song = null;
		int newId = id;
		
		System.out.println(""+ newId);
		
		try
		{
			//Connecting to database
			conn = DriverManager.getConnection(url, username, password);
			
			//Execute SQL Query and loop
			Statement stmt = conn.createStatement();
			ResultSet rs = stmt.executeQuery(sql);
			
			while(rs.next())
			{
				
				//add new song with each new table
				if (rs.getInt("ID") == newId)
				{
					
					song = new Song(rs.getInt("ID"),
							rs.getInt("SONG_NO"),
							rs.getString("SONG_NAME"), 
							rs.getString("SONG_ALBUM"), 
							rs.getString("SONG_ARTIST"),
							rs.getString("SONG_GENRE"));
				}
			}
			
			
			conn.close();
			return song;
		}
		catch (SQLException e)
		{
			
			e.printStackTrace();
			return null;
			
		}
		finally
		{
			//Database cleaning
			if(conn != null)
			{
				try
				{
					conn.close();
				}
				catch (SQLException e)
				{
					e.printStackTrace();
				}
			}
			
		}
    }
    
    
    
    
    
	/**
     *Tests to see if class is working
     * @Param int
     */
    public void test()
    {
    	System.out.println("test");
    }

    
	/**
     * Returns song based on the ID in the Database. If ID is empty then return null.
     * @Param Song
     */
    public List<Song> search(Song song) {
        // TODO Auto-generated method stub
		Connection conn = null;	
		String sql = "SELECT * FROM milestone.SONGS";
		List<Song> songs = new ArrayList<Song>();
		
		try
		{
			//Connecting to database
			conn = DriverManager.getConnection(url, username, password);
			
			//Execute SQL Query and loop
			Statement stmt = conn.createStatement();
			ResultSet rs = stmt.executeQuery(sql);
			
			while(rs.next())
			{
				if (rs.getString("SONG_NAME").equals(song.getName()))
				{
				//add new order with each new table
				songs.add(new Song(rs.getInt("ID"),
						rs.getInt("SONG_NO"),
						rs.getString("SONG_NAME"), 
						rs.getString("SONG_ALBUM"), 
						rs.getString("SONG_ARTIST"),
						rs.getString("SONG_GENRE")));	
				}
			}
			
			
			conn.close();
			return songs;
		}
		catch (SQLException e)
		{
			
			e.printStackTrace();
			return null;
			
		}
		finally
		{
			//Database cleaning
			if(conn != null)
			{
				try
				{
					conn.close();
				}
				catch (SQLException e)
				{
					e.printStackTrace();
				}
			}
			
		}
    }
    
    
    
	/**
     * Makes a list of every song in the genre
     * @Param Song
     */
    public List<Song> byGenre(Song song){
    	
    	List<Song> songs = new ArrayList<Song>();
		Connection conn = null;	
		String sql = "SELECT * FROM milestone.SONGS WHERE SONG_GENRE = '"
				+ song.getGenre() + "'";
		
		
		try
		{
			//Connecting to database
			conn = DriverManager.getConnection(url, username, password);
			
			//Execute SQL Query and loop
			Statement stmt = conn.createStatement();
			ResultSet rs = stmt.executeQuery(sql);
			
			while(rs.next())
			{

				//add new order with each new table
				songs.add(new Song(rs.getInt("ID"),
						rs.getInt("SONG_NO"),
						rs.getString("SONG_NAME"), 
						rs.getString("SONG_ALBUM"), 
						rs.getString("SONG_ARTIST"),
						rs.getString("SONG_GENRE")));	
				
			}
			
			
			conn.close();
			return songs;
		}
		catch (SQLException e)
		{
			
			e.printStackTrace();
			return null;
			
		}
		finally
		{
			//Database cleaning
			if(conn != null)
			{
				try
				{
					conn.close();
				}
				catch (SQLException e)
				{
					e.printStackTrace();
				}
			}
		}
    
    }
}
