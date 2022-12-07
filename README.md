# Elite Dangerous Addd On Helper
The purpose of this program is to allow you to choose which apps and websites to launch when you run Elite, and optionally close the apps when you close the Elite launcher. Ive included a few common ones as an example, you can delete or edit these as you require, and add new apps or websites.

First Beta is now available!

![image](https://user-images.githubusercontent.com/5197831/206279174-bcd98042-9ee2-41f7-81ad-ebdabfc8582f.png)

This should be pretty self-explanatory,
*If an app is highlighted in red this means its path is not found / its not installed.
*Apps in green are discovered installed apps or URLs

# Add App

![image](https://user-images.githubusercontent.com/5197831/206279306-e7abf380-59d0-48f4-b096-c8f1320d0369.png)

The add app screen only has a couple of mandatory fields, depending on if you are adding a 3rd party app such as EDMC, or adding a URL like inara

For apps, application name in mandatory and should be unique. Then click the button to browse to the exe file - the next two fields (application path and executable name) will be filled in for you.
Some apps (TARGET for example) required arguments. This field is here for your convenience. HINT To launch Elite in vr mode, add /VR as an argument to Edlaunch

Installation URL is for dev purposes really, you can populate it with a link to the apps installer on the web, of limited use and I may remove this in future versions
WebApp URL - when you type in this, the Application Path, Exe name and buttons are disabled, as they server no purpose when launching a URL. Urls will be opened in your default browser.

# Edit App

![image](https://user-images.githubusercontent.com/5197831/206279617-98671568-dd8d-43a6-96b3-816fb460d672.png)

This is almost identical to the Add App screen, with the exception of the Autodiscver URL, which will be removed in future releases.


Other options on the main form you can see are Close Apps on Exit, which will cause the app to watch the Elite Launcer, and when it closes will close all apps it has opened. It will not however close websites it opened (May be added in future releases)
You can also toggle between VR/Non VR for those players that flip between the two mode depending on their gameplay style.
