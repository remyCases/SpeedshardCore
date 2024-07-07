call.i gml_Script_scr_psychic_accumulation(argc=3)
popz.v

push.s "LVL"
conv.s.v
call.i gml_Script_scr_atr(argc=1)
pushglb.v global.interval_level_extra_ap
mod.v.v
pushi.e 0
cmp.i.v EQ
bf [801]

:[800]
pushi.e 2
conv.i.v
push.s "AP"
conv.s.v
call.i gml_Script_scr_atr_incr(argc=2)
popz.v
b [802]

:[801]
pushi.e 1
conv.i.v
push.s "AP"
conv.s.v
call.i gml_Script_scr_atr_incr(argc=2)
popz.v

:[802]
push.s "LVL"
conv.s.v
call.i gml_Script_scr_atr(argc=1)
pushglb.v global.interval_level_extra_sp
mod.v.v
pushi.e 0
cmp.i.v EQ
bf [804]

:[803]
pushi.e 2
conv.i.v
push.s "SP"
conv.s.v
call.i gml_Script_scr_atr_incr(argc=2)
popz.v
b [805]

:[804]
pushi.e 1
conv.i.v
push.s "SP"
conv.s.v
call.i gml_Script_scr_atr_incr(argc=2)
popz.v

:[805]