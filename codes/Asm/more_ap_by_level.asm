push.s "LVL"
conv.s.v
call.i gml_Script_scr_atr(argc=1)
pushglb.v global.interval_level_extra_sp
mod.v.v
pushi.e 0
cmp.i.v EQ
bf [801]

:[800]
pushi.e 1
conv.i.v
push.s "AP"
conv.s.v
call.i gml_Script_scr_atr_incr(argc=2)
popz.v

:[801]