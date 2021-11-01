package beans;

import java.io.FileWriter;
import java.io.IOException;
import java.security.Principal;

import javax.faces.bean.ManagedBean;
import javax.faces.bean.ViewScoped;
import javax.faces.context.FacesContext;
import javax.validation.constraints.NotNull;
import javax.validation.constraints.Size;

@ManagedBean
@ViewScoped
public class User {

		//Initializing the values and adding Bean validation constraints
		@NotNull(message = "Please enter a First Name. This is a required field")
		@Size(min=4, max=15)
		String firstName = "";

		@NotNull(message = "Please enter a Last Name. This is a required field")
		@Size(min=4, max=15)
		String lastName = "";

		@NotNull(message = "Please enter an email. This is a required field")
		@Size(min=10, max=20)
		String email  = "";

		@NotNull(message = "Please enter an address. This is a required field")
		@Size(min=10, max=25)
		String address = "";

		@NotNull(message = "Please enter your phone number. This is a required field")
		@Size(min=4, max=15)
		String phoneNumber = "";
	
		@NotNull(message = "Please enter a Username. This is a required field")
		@Size(min=4, max=15)
		String userName = "";
		
		@NotNull(message = "Please enter a password. This is a required field")
		@Size(min=8, max=15)
		String password ="";
		
		/**
		 * User Default Constructor
		 *
		 *
		 */
		public User() 
		{
			firstName = "first name";
			lastName = "last name";
			email = "name@example.com";
			address = "3000 west Ln";
			phoneNumber = "phone number";
			userName = "username";
			password ="password";
		}
		
		
		/**
		 * Sets first name to the name in the principle or unkown.
		 *
		 *
		 */
		public void init()
		{
			// Get the logged in Principle
			Principal principle= FacesContext.getCurrentInstance().getExternalContext().getUserPrincipal();
				if(principle == null)
				{
					setFirstName("Unknown");
					setLastName("");
				}
				else
				{
					setFirstName(principle.getName());
					setLastName("");
				}

		}
		
		//getter and setters
		
		
		public String getFirstName() {
		return firstName;
		}
		
		public void setFirstName(String firstName) {
			this.firstName = firstName;
		}
		
		//getter and setter for lastname
		public String getLastName() {
			return lastName;
		}

		public void setLastName(String lastName) {
			this.lastName = lastName;
		}
	
		//getter and setter for email
		public String getEmail() {
			return email;
		}

		public void setEmail(String email) {
			this.email = email;
		}

		//getter and setter for address
		public String getAddress() {
			return address;
		}
		
		public void setAddress(String address) {
			this.address = address;
		}
		
	        //getter and setter for phoneNumber
		public String getPhoneNumber() {
			return phoneNumber;
		}

		public void setPhoneNumber(String phoneNumber) {
			this.phoneNumber = phoneNumber;
		}
		
		//getter and setter for userName
		public String getUserName() {
			return userName;
		}

		public void setUserName(String userName) {
			this.userName = userName;
		}
		
		//getter and setter for password
		public String getPassword() {
			return password;
		}
		
		public void setPassword(String password) {
			this.password = password;
		}


		/**
		 * Creates a text file writes to all the properties to the file.
		 *
		 *
		 */
		public void saveData() 
		{
			try 
			{
				FileWriter writer = new FileWriter("RegistrationFile.txt");

				writer.write(this.getFirstName() + "\n" + this.getLastName() + "\n" + this.getEmail() + "\n" + this.getAddress()
							 + "\n"+ this.getPhoneNumber()+ "\n"+ this.getUserName()+"\n"+this.getPassword());
				System.out.println("Successfully wrote to the file");
				writer.close();

			}
			catch(IOException e) 
			{
				System.out.println("Error occured file wasn't created");
				e.printStackTrace();
			}	
		}
		
		
}
