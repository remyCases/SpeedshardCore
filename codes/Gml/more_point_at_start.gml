ini_open("speedshard.ini")
global.extra_sp_ap_at_creation = ini_read_real("globals", "extra_sp_ap_at_creation", 0)
ini_close()
if global.extra_sp_ap_at_creation
{
    ds_map_add(global.characterDataMap, "AP", 1)
    ds_map_add(global.characterDataMap, "SP", 3)
}
else
{
    ds_map_add(global.characterDataMap, "AP", 0)
    ds_map_add(global.characterDataMap, "SP", 2)
}