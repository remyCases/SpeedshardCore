if (global.gamespeed == global.defaut_gamespeed)
    global.gamespeed = global.accelerated_gamespeed
else
    global.gamespeed = global.defaut_gamespeed
game_set_speed(global.gamespeed, gamespeed_fps)