:[28]
pushi.e 3
conv.i.v
pushi.e 2
conv.i.v
pushi.e 1
conv.i.v
pushi.e 0
conv.i.v
call.i choose(argc=4)
push.d 0.35
push.v self.Wilderness
pushi.e 100
conv.i.d
div.d.v
add.v.d
mul.v.v
push.s "LVL"
conv.s.v
call.i gml_Script_scr_atr(argc=1)
pushglb.v global.hostile_spawnrate_by_level_modifier 
div.v.v
add.v.v
pop.v.v self.HostileSlots
pushi.e 5
conv.i.v
pushi.e 4
conv.i.v
pushi.e 3
conv.i.v
pushi.e 2
conv.i.v
call.i choose(argc=4)
push.d 0.35
push.v self.Wilderness
pushi.e 100
conv.i.d
div.d.v
add.v.d
mul.v.v
push.s "LVL"
conv.s.v
call.i gml_Script_scr_atr(argc=1)
pushglb.v global.neutral_spawnrate_by_level_modifier 
div.v.v
add.v.v
pop.v.v self.NeutralSlots