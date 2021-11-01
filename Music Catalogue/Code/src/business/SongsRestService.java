package business;

import java.util.List;

import javax.enterprise.context.RequestScoped;
import javax.inject.Inject;
import javax.ws.rs.Consumes;
import javax.ws.rs.GET;
import javax.ws.rs.POST;
import javax.ws.rs.Path;
import javax.ws.rs.Produces;
import javax.ws.rs.core.MediaType;

import beans.Song;


@RequestScoped
@Path("/songs")
@Produces("application/json")
@Consumes("application/json")
public class SongsRestService {
	
		//Insert the interface
		@Inject
		SongBusinessInterface service;
		
	
		
		/**
	     *Returns a list of the songs with the /getjson url
	     *
	     *
	     */
		@GET
		@Path("/getjson")
		@Produces(MediaType.APPLICATION_JSON)
		public List<Song> getSongsAsJson()
		{
			return service.getSongs();
			
		}
		
		
		
		/**
	     *Returns the song that was added to the database with the /postjson url
	     *
	     *
	     */
		@POST
		@Path("/postjson")
		@Produces(MediaType.APPLICATION_JSON)
		public Song creatSongAsJson(Song song)
		{
			service.addSong(song);
			return song;
			
		}

}
