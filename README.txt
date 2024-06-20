# Week5

2024-06-06 0143 EDT
Gamma Group
Review the 'Product' assignment and investigate this real world object for specific properties, structure and various attributes

0320- we read the assignments and project work individually 

0325- Shared what we understood from what we read individually 

0332- Brainstorming on ¡°COMPANY NAME¡±. First task

0338- Came about with the name Gamma Wears, we concluded with that and moving forward. We need a scope, 

Color
Material
Sex available¡ª males/ Female
Categories¡ª kids/adult 
Size- large/small
Texture- thick/light 



# Week 6

2024-06-08 8410 UTC

Discuss the requirements for the Gamma Wear website. How should the UI look like?

0410 - Search the website and decide from which website to get the properties. Choose the website https://greatsox.com/.

7610 - Set the properties table.

4810 - Create the project and set the data base. Register and Login works.

2010 - Home Page, Create, Edit, Delete works.



2024-06-11 0210 EDT

2024-06-13 14:00 EDT

14:00: Made the site public on github and tested on all three computers(team members) together.
14:30: Got the students free plan on Azure and tried to upload the site as a web app
14:50: Made it work gammaweargammawear.azurewebsites.net except the home page.
14:50: Site worked fine when tried to login, create user and even on about page but main/home page did not work.
15:10: Tried to update site using different creds such as my computer but my team mate's github(primary repo) but the same error at the end.

15:15: Dropped the idea of creating it on azure wasn't mandatory but preferred for this assignment(at least for now)

15:20 edited the edit.cshtml file and add some more socks images to the app.. error loading created another folder git.cshtl with the same content 
runtime error on all 3 computers, that means the problem seemed to be in the code .. had to delelte and recreate some of the file. such as cshmtl files

15:25 Synced the files and the site worked now on the local host 
It still didnnt work on other computers, had to update the database and performed the sync again using nutget packet manager

15:28 issues sorted out on all computers.


2024-06-20 11:01 EDT

11:02- We tested the application locally on three computers, tested the register and login options(auth) with admin rights and this worked fine. Registered credentials can also login with no issues/error. 


11:45- The product list, product images and ratings were also tested on each computer locally. This also worked fine on each of the 3 computers we tested the application on. 
In conclusion to this, locally our application works fine.


12:50- Deployment: we setup the ‘about’ page of our application on Azure and this worked fine. Displayed right! But the product page showed three types of error. i.e
	Error 500
	Win32exception
	SQL exception


2:35-   After troubleshooting we believe this a deployment issue as the application works fine locally. NOTE: The issue is limited to the product page only, as the ‘about’ page works fine. We have included the links below to support this, 

Website that worked: 
https://gammaweargammawear.azurewebsites.net/home/about

The Azure service is stopped to save credits (students free plan) but can enable it if/when requested.

Website that did not work:
https://gammaweargammawear.azurewebsites.net/



