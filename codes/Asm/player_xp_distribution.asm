push.s "LVL"
conv.s.v
call.i gml_Script_scr_atr(argc=1)
pushglb.v global.xp_by_level_modifier 
mul.v.v
pop.v.v self.max_xp