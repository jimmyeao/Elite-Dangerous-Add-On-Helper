# Elite Dangerous Addd On Helper
The purpose of this program is to allow you to choose which apps and websites to launch when you run Elite, and optionally close the apps when you close the Elite launcher. Ive included a few common ones as an example, you can delete or edit these as you require, and add new apps or websites.

First Beta is now available!

![image](https://user-images.githubusercontent.com/5197831/204605658-a024b0c5-5a90-4859-85ea-dfc8cae993dd.png)

This should be pretty self-explanatory,
*If an app is highlighted in red this means its path is not found / its not installed.
*Apps in green are discovered installed apps or URLs

# Add App

![image](https://user-images.githubusercontent.com/5197831/204606365-d0ff6603-07ab-43f8-b815-ce60c93eb2c6.png)

The add app screen only has a couple of mandatory fields, depending on if you are adding a 3rd party app such as EDMC, or adding a URL like inara

For apps, application name in mandatory and should be unique. Then click the button to browse to the exe file - the next two fields (application path and executable name) will be filled in for you.
Some apps (TARGET for example) required arguments. This field is here for your convenience, but unless you are running TARGET you probably wont need this
Installation URL is for dev purposes really, you can populate it with a link to the apps installer on the web, of limited use and I may remove this in future versions
WebApp URL - when you type in this, the Application Path, Exe name and buttons are disabled, as they server no purpose when launching a URL. Urls will be opened in your default browser.

# Edit App

![image](https://user-images.githubusercontent.com/5197831/204809831-0a4c6564-60b8-495e-a1be-8d60c64c6407.png)

This is almost identical to the Add App screen, with the exception of the Autodiscver URL, which will be removed in future releases.


Other options on the main form you can see are Close Apps on Exit, which will cause the app to watch the Elite Launcer, and when it closes will close all apps it has opened. It will not however close websites it opened (May be added in future releases)
You can also toggle between VR/Non VR for those players that flip between the two mode depending on their gameplay style.
