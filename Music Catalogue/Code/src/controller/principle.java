package controller;


import javax.ejb.EJB;
import javax.faces.application.FacesMessage;
import javax.faces.bean.ManagedBean;
import javax.faces.bean.ViewScoped;
import javax.faces.context.FacesContext;
import javax.inject.Inject;


//Imports user class
import beans.User;
import business.SongBusinessInterface;
import business.UserBusinessInterface;
import beans.Song;



@ManagedBean
@ViewScoped
public class principle {

	
	@Inject
	UserBusinessInterface userService;
	
	@EJB
	SongBusinessInterface service;
	
	
	/**
     * Logs the user off and sends them to the login screen
     */
	public String onLogOff()
	{
		//Get the User Managed bea
		
		//Forward to Test Response View along with the user Managed Bean
		FacesContext.getCurrentInstance().getExternalContext().invalidateSession();
		return "Main.xhtml?faces-redirect=true";
	}
	
	
	/**
     * Submits button that checks if username and password match
     * @Param User
     */
	public String onSubmit(User user)
	{
		
		
		//if both passwords are the same then forward to main page
		if (userService.checkPassword(user))
		{
			

			//Forward to test response view with the user managed bean.
			FacesContext.getCurrentInstance().getExternalContext().getRequestMap().put("user", user);
			return "Main.xhtml";
		}
		//If password is not the same then tell customer unknown information.
		else
		{
			
			FacesContext.getCurrentInstance().addMessage(null, new FacesMessage("Unknown username or password"));
			return "Login.xhtml";
		}
		
	}
	
	/**
     * For update button
     * @Param Song
     */
	public String onUpdate(Song song)
	{
		//Forward response view with the song managed bean.
		FacesContext.getCurrentInstance().getExternalContext().getRequestMap().put("song", song);
		return "UpdateSong.xhtml";
	}
	
	
	/**
     * For detailed button
     * @Param Song
     */
	public String onDetailed(Song song)
	{
		//Forward response view with the song managed bean.
		FacesContext.getCurrentInstance().getExternalContext().getRequestMap().put("songs", song);
		return "DetailSong.xhtml";
	}
	
	/**
     * For delete button
     * @Param Song
     */
	public String onDelete(Song song) 
	{
		
		service.deleteSong(song);
		
		return "DisplaySong.xhtml";
	}
	
	/**
     * For DeleteHelp button
     * @Param Song
     */
	public String onDeleteHelp(Song song) 
	{
		FacesContext.getCurrentInstance().getExternalContext().getRequestMap().put("song", song);
		return "Delete.xhtml";
	}
	
	public String onSearch(Song song)
	{
		
		
		FacesContext.getCurrentInstance().getExternalContext().getRequestMap().put("searched", song);
		return "Search2.xhtml";
	}
	
	/**
     * For Randomize button
     * @Param Song
     */
	public String onRandom(Song song)
	{
		//Forward response view with the song managed bean.
		FacesContext.getCurrentInstance().getExternalContext().getRequestMap().put("song", song);
		return "RandomPicks2.xhtml";
	}
	
	
	/**
     * Button that adds a song to the product list
     * @Param Song
     */
	public String addProduct(Song song)
	{
		//adds song from the values added in the get button page.
		service.addSong(song);
		

		
		return "DisplaySong.xhtml";
	}


	/**
     * Button that changes a song to the product list
     * @Param Song
     */
	public String changeProduct(Song song)
	{


		System.out.println(""+song.getId());
		//Changes song with values from the 
		service.changeSong(song);
		

		
		return "DisplaySong.xhtml";
	}
	

	
	public UserBusinessInterface getService1()
	{
		return userService;
	}
	
	public SongBusinessInterface getService()
	{
		return service;
	}
	
}

