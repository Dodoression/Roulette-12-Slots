# Roulette With 12 Prize Slots
Simple roulette windows application made in Unity Engine, using 12 prize slots with customizable names, amounts, chances and sprites.

---
# Customizable Sprites
To customize the sprites, when making a build, in the build folder, head to `"Roulette_Data" -> "StreamingAssets"`.
There, you will find different placeholder sprites for the background, roulette wheel, logo/company central sprite, pointer, button and win panel.
The following should be named strictly with the same names and file extensions. It is recommended to use the same image size / resolution, as following:
```
Background.png     - 1080x1920

Logo_Center.png    - 1080x1080
Pointer.png        - 1080x1080
Roulette.png       - 1080x1080
Start_Button.png   - 1080x1080
Win_Frame.png      - 1080x1080
```

---
# Setting Up The Prizes
In the same folder, `"Roulette_Data" -> "StreamingAssets"`, you will find a JSON-format file, named `prizes.json`.
In the file, you will find different elements, named "items". Each element and it's properties can be modified.
```
ID        - the prize's ID. It's recommended not to modify this property, unless you really need to. 
name      - the name, shown on the roulette wheel. Maximum of 16 symbols, including spaces.
amount    - the amount of prizes.
weight    - the weight of each prize. There is no range (for example 0-1, 0%-100%, etc). 
The proportion of each prize is calculated based on the sum of all prizes, divided by their count.
```

---
# Additional In-App Features
`RESET BUTTON` - Holding the upper left edge of the screen for 3 seconds resets the amount of each prize.
An indicator for a successful reset is a text that appears for a short amount of time, signaling the resetting, and dissappearing afterwards.
<br />`QUIT BUTTON` - Holding the upper right edge of the screen for 5 seconds quits the application.
