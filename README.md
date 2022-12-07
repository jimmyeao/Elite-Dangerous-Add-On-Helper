# Elite Dangerous Addd On Helper
The purpose of this program is to allow you to choose which apps and websites to launch when you run Elite, and optionally close the apps when you close the Elite launcher. Ive included a few common ones as an example, you can delete or edit these as you require, and add new apps or websites.

First Beta is now available!

![image](https://user-images.githubusercontent.com/5197831/206279174-bcd98042-9ee2-41f7-81ad-ebdabfc8582f.png)

This should be pretty self-explanatory,
*If an app is highlighted in red this means its path is not found / its not installed.
*Apps in green are discovered installed apps or URLs
You can now create profiles to suit your style of play, you may want different addons / websites loaded for combat, mining, exploration etc

# Add App

![image](https://user-images.githubusercontent.com/5197831/206279805-dc599b10-6645-4099-b94f-804df4a1db11.png)

The add app screen only has a couple of mandatory fields, depending on if you are adding a 3rd party app such as EDMC, or adding a URL like inara

For apps, application name in mandatory and should be unique. Then click the button to browse to the exe file - the next two fields (application path and executable name) will be filled in for you.
Some apps (TARGET for example) required arguments. This field is here for your convenience. HINT To launch Elite in vr mode, add /VR as an argument to Edlaunch

Installation URL is for dev purposes really, you can populate it with a link to the apps installer on the web, of limited use and I may remove this in future versions
WebApp URL - when you type in this, the Application Path, Exe name and buttons are disabled, as they server no purpose when launching a URL. Urls will be opened in your default browser; Picture files will be opened in your default picture viewer etc. In fact, anyhing in here will be launced with its associated app!

# Edit App

![image](https://user-images.githubusercontent.com/5197831/206280130-ef747763-51a6-4659-a677-dab638ebecb5.png)

This is almost identical to the Add App screen and should be self explanatory :)


Other options on the main form you can see are Close Apps on Exit, which will cause the app to watch the Elite Launcer, and when it closes will close all apps it has opened. It will not however close websites it opened (May be added in future releases)

