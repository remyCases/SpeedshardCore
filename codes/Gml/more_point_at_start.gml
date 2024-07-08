if global.extra_sp_ap_at_creation
{
    ds_map_replace(global.characterDataMap, "AP", ds_map_find_value(global.characterDataMap, "AP") + 1)
    ds_map_replace(global.characterDataMap, "SP", ds_map_find_value(global.characterDataMap, "SP") + 1)
}