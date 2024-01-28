# NATO ERA v1.1

## Features:
<p>
	<ul>
	<li>Adds configurable BRAT ERA to M2 Bradley</li>
	<li>Currently modelled the hull array</li>
	<li>Configurable ERA arrangement</li>
	<li>Configurable ERA performance</li>
	<li>Configurable ERA color (so you can match it to the custom skins you have)</li>
	<li>Compatible with vanilla Bradley, and 50mm Bradley, M3A3 Bradley and M6A2 ADATS mods</li>
	<li>Only the BRAT M3 and M5 bricks are modelled, not the custom skirts or anything else to minimize potential performance hit</li>
	<li>Triangular pieces such as the M4 brick is not modelled since triangles are not native to Unity Editor</li>
 	</ul>
</p>


![BRAT Standard Array](https://github.com/Cyances/NATO-ERA/blob/NERA-1.1/Images/Standard%20BRAT.png)

## ERA Performance:
| Type  | Protection vs KE (mm) | Protection vs CE (mm) | Note
| ------------- | ------------- | ------------- | ------------- | 
| BRAT-M3(M) | 30  | 450  | Front hull and extra arrays |
| BRAT-M5(M) | 45  | 600  | Side hull |
| Improved BRAT-M3(M) | 30  | 450  | Can be toggled separately |
| Improved BRAT-M5(M) | 45  | 600  | Can be toggled separately |


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
