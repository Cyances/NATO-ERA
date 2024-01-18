# NATO ERA v1.0 (Initial Release)

Special thanks to thebeninator for the coding and modelling advice for this mod.

## Features:
<p>
	<ul>
	<li>Adds configurable ARAT-1 ERA to M1/IP Abrams</li>
	<li>Configurable ERA arrangement</li>
	<li>Configurable ERA performance</li>
	<li>Configurable ERA color (so you can match it to the custom skins you have)</li>
	<li>Compatible with vanilla Abrams, and modded M1A1 Abrams and M1A1 Abrams AMP</li>
 	</ul>
</p>


![ARAT-1 Standard Array](https://github.com/Cyances/NATO-ERA/blob/main/Images/Standard%20ARAT-1.png)
![ARAT-1 Arrangements](https://github.com/Cyances/NATO-ERA/blob/main/Images/ARAT-1%20Arrangements.png)
![Flork Spec](https://github.com/Cyances/NATO-ERA/blob/main/Images/Flork%20Spec.png)
![Indigo ARAT](https://github.com/Cyances/NATO-ERA/blob/main/Images/Indigo%20ARAT-1.png)

## Installation:
1.) Install [MelonLoader](https://github.com/LavaGang/MelonLoader/).

2.) Download NatoERA.dll and NatoEraAssets.tar or .zip from the [release page](https://github.com/Cyances/NATO-ERA/releases).

3.) Rename NatoERA.dll to <b>!NatoERA.dll</b> (note the exclamation mark at the start). This is important for mod compatibility.

4.) Place the files in the manner below:

![Mods Folder](https://github.com/Cyances/NATO-ERA/blob/main/Images/Mods%20Folder.PNG)
![NatoAssetsEra Folder](https://github.com/Cyances/NATO-ERA/blob/main/Images/NatoEraAssets%20Folder.PNG)

5.) Launch the game.
   
7.) On first time running this mod, the entries in MelonPreferences.cfg will only appear after launching the game then closing it.

## ERA Performance:
| Type  | Protection vs KE (mm) | Protection vs CE (mm) | Note
| ------------- | ------------- | ------------- | ------------- | 
| ARAT-1(M) | 30  | 450  | M version is half size |
| Improved ARAT-1(M) | 150  | 600  | Can be toggled separately |


## Mod Configuration (in UserData/MelonPreferences.cfg):

| Option  | Default config | Note
| ------------- | ------------- | ------------- | 
| M1 ERA  | true | Hull sides array. Also master option to show ERA or not regardless of config below. |
| M1 Hull Lower Front ERA  | false  |  |
| M1 Hull Upper Front ERA  | false  |  |
| M1 Extended Hull ERA | false  |  |
| M1 Turret ERA | false | Turret sides array. Also master option to show other turret ERA arrays or not regardless of config below. |
| M1 Turret Roof ERA | false |  |
| M1 Turret Cheek ERA | false |  |
| M1 Extended Turret ERA | false |  |
| - | - | - |
| M1IP ERA  | true | Hull sides array. Also master option to show ERA or not regardless of config below. |
| M1IP Hull Lower Front ERA  | false  |  |
| M1IP Hull Upper Front ERA  | false  |  |
| M1IP Extended Hull ERA | false  |  |
| M1IP Turret ERA | false | Turret sides array. Also master option to show other turret ERA arrays or not regardless of config below. |
| M1IP Turret Roof ERA | false |  |
| M1IP Turret Cheek ERA | false |  |
| M1IP Extended Turret ERA | false |  |
| - | - | - |
| Improved CE Protection | false |  |
| Improved KE Protection | false |  |
| ARAT color | 71, 80, 65 | RGB values. Army green as default. |

The ERA arrangment for the Abrams can be changed when (re)loading missions without restarting the game. Simply alt tab to edit the .cfg file, save the file then restart the mission or start a new one. However, to see updated ARAT performance or color, the game needs to be restarted.

![Configuration File](https://github.com/Cyances/NATO-ERA/blob/main/Images/NATO%20ERA%20MelonPreferences.PNG)
