# NATO ERA v1.1.4

Special thanks to thebeninator for the coding and 3d modelling advice for this mod.

## Features:
<p>
	<ul>
	<li>Adds configurable ERA to M1/IP Abrams and M2 Bradley</li>
	<li>Configurable ERA arrangement</li>
	<li>Configurable ERA performance</li>
	<li>Configurable ERA color (so you can match it to the custom skins you have)</li>
	<li>Compatible with vanilla Abrams, M1A1 Abrams v1.1.4, M1A1 Abrams AMP v2.3.3, 50mm Bradley, M3A3 Bradley* and M6A2 ADATS mods</li>
	<li>Only the ARAT-1, BRAT M3 and M5 bricks are modelled, not the custom skirts or anything else to minimize potential performance hit</li>
	<li>Triangular pieces such as the M4 brick is not modelled since triangles are not native to Unity Editor</li>
 	</ul>
</p>

## NOTE!
<p>
	<ul>
	<li>If you want to apply your own textures to the ERA, you may approach me for the required model files</li>
	<li>If you are also using the M3A3 mod, use 1NatoERA_M3.dll instead for compatibility. If you are using other/no Bradley mods, use the non-M3.dll</li>
 	</ul>
</p>


![ARAT-1 Standard Array](https://github.com/Cyances/NATO-ERA/blob/main/Images/Standard%20ARAT-1.png)
![BRAT Standard](https://github.com/Cyances/NATO-ERA/blob/main/Images/Standard%20BRAT.png)
![Flork Spec](https://github.com/Cyances/NATO-ERA/blob/main/Images/Flork%20Spec.png)
![Indigo ARAT](https://github.com/Cyances/NATO-ERA/blob/main/Images/Indigo%20ARAT-1.png)

## Installation:
1.) Install [MelonLoader](https://github.com/LavaGang/MelonLoader/).

2.) Download NATO ERA v#.#.tar or .zip from the [release page](https://github.com/Cyances/NATO-ERA/releases).

3.) Extract the the files and place them in the manner below:

![Mods Folder](https://github.com/Cyances/NATO-ERA/blob/main/Images/Mods%20Folder.PNG)
![NatoAssetsEra Folder](https://github.com/Cyances/NATO-ERA/blob/main/Images/NatoEraAssets%20Folder%20v2.PNG)

4.) Launch the game.
   
5.) On first time running this mod, the entries in MelonPreferences.cfg will only appear after launching the game then closing it.

## ERA Performance:
| Type  | Protection vs KE (mm) | Protection vs CE (mm) | Note
| ------------- | ------------- | ------------- | ------------- | 
| ARAT-1(M) | 30  | 450  | M version is half size |
| Improved ARAT-1(M) | 150  | 600  | Can be toggled separately |
| BRAT-M3(M) | 30  | 450  | Front hull and extra arrays |
| BRAT-M5 | 45  | 600  | Side hull |
| Improved BRAT-M3(M) | 150  | 600  | Can be toggled separately |
| Improved BRAT-M5 | 200  | 800  | Can be toggled separately |


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
| - | - | - |
| M2 ERA  | true | Hull sides and front array. Master option to show ERA or not regardless of config below. |
| M2 Extended Hull ERA | false  |  |
| M2 Side Lower  ERA  | true  |  |
| M2 Extended Side Lower  ERA  | false  |  |
| M2 Hull Lower Front ERA  | false  |  |
| M2 Hull Upper ERA  | false  |  |
| - | - | - |
| Improved BRAT-M3 CE Protection | false |  |
| Improved BRAT-M3 KE Protection | false |  |
| Improved BRAT-M5 CE Protection | false |  |
| Improved BRAT-M5 KE Protection | false |  |
| ARAT color | 71, 80, 65 | RGB values. Army green as default. |

The ERA arrangment for the units can be changed when (re)loading missions without restarting the game. Simply alt tab to edit the .cfg file, save the file then restart the mission or start a new one. However, to see updated ARAT performance or color, the game needs to be restarted.

![Configuration File](https://github.com/Cyances/NATO-ERA/blob/main/Images/NATO%20ERA%20MelonPreferences%20v2.PNG)
