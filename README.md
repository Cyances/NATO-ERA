# NATO ERA v1.1

## Features:
<p>
	<ul>
	<li>Adds configurable BRAT ERA to M2 Bradley</li>
	<li>Currently modelled the hull array</li>
	<li>Configurable ERA arrangement</li>
	<li>Configurable ERA performance</li>
	<li>Configurable ERA color (so you can match it to the custom skins you have)</li>
	<li>Compatible with vanilla Bradley, </li>
	<li>Only the BRAT M3 and M5 bricks are modelled, not the custom skirts or anything else to minimize potential performance hit</li>
	<li>Triangular pieces such as the M4 brick is not modelled since triangles are not native to Unity Editor</li>
	<li>Allows anti-ERA penetrator configuration (for modders)</li>
	<li>Compatible with 50mm Bradley, M3A3 Bradley and M6A2 ADATS mods, but not vanilla Bradley</li>
 	</ul>
</p>


![BRAT Standard Array](https://github.com/Cyances/NATO-ERA/blob/NERA-1.1/Images/Standard%20BRAT.png)

## ERA Performance:
| Type  | Protection vs KE (mm) | Protection vs CE (mm) | Note
| ------------- | ------------- | ------------- | ------------- | 
| BRAT-M3(M) | 30  | 450  | Front hull and extra arrays |
| BRAT-M5 | 45  | 600  | Side hull |
| Improved BRAT-M3(M) | 150  | 600  | Can be toggled separately |
| Improved BRAT-M5 | 200  | 800  | Can be toggled separately |


## Mod Configuration (in UserData/MelonPreferences.cfg):

| Option  | Default config | Note
| ------------- | ------------- | ------------- | 
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

The ERA arrangment can be changed when (re)loading missions without restarting the game. Simply alt tab to edit the .cfg file, save the file then restart the mission or start a new one. However, to see updated ERA performance or color, the game needs to be restarted.

![Configuration File](https://github.com/Cyances/NATO-ERA/blob/main/Images/NATO%20ERA%20MelonPreferences.PNG)
