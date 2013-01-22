#if defined(__arm__)
.section __DWARF, __debug_abbrev,regular,debug

	.byte 1,17,1,37,8,3,8,27,8,19,11,17,1,18,1,16,6,0,0,2,46,1,3,8,17,1,18,1,64,10,0,0
	.byte 3,5,0,3,8,73,19,2,10,0,0,15,5,0,3,8,73,19,2,6,0,0,4,36,0,11,11,62,11,3,8,0
	.byte 0,5,2,1,3,8,11,15,0,0,17,2,0,3,8,11,15,0,0,6,13,0,3,8,73,19,56,10,0,0,7,22
	.byte 0,3,8,73,19,0,0,8,4,1,3,8,11,15,73,19,0,0,9,40,0,3,8,28,13,0,0,10,57,1,3,8
	.byte 0,0,11,52,0,3,8,73,19,2,10,0,0,12,52,0,3,8,73,19,2,6,0,0,13,15,0,73,19,0,0,14
	.byte 16,0,73,19,0,0,16,28,0,73,19,56,10,0,0,0
.section __DWARF, __debug_info,regular,debug
Ldebug_info_start:

	.long Ldebug_info_end - Ldebug_info_begin
Ldebug_info_begin:

	.short 2
	.long 0
	.byte 4,1
	.asciz "Mono AOT Compiler 2.6.5 (tarball Tue Jul 31 12:12:14 CEST 2012)"
	.asciz "JITted code"
	.asciz ""

	.byte 2,0,0,0,0,0,0,0,0
	.long Ldebug_line_start - Ldebug_line_section_start
LDIE_I1:

	.byte 4,1,5
	.asciz "sbyte"
LDIE_U1:

	.byte 4,1,7
	.asciz "byte"
LDIE_I2:

	.byte 4,2,5
	.asciz "short"
LDIE_U2:

	.byte 4,2,7
	.asciz "ushort"
LDIE_I4:

	.byte 4,4,5
	.asciz "int"
LDIE_U4:

	.byte 4,4,7
	.asciz "uint"
LDIE_I8:

	.byte 4,8,5
	.asciz "long"
LDIE_U8:

	.byte 4,8,7
	.asciz "ulong"
LDIE_I:

	.byte 4,4,5
	.asciz "intptr"
LDIE_U:

	.byte 4,4,7
	.asciz "uintptr"
LDIE_R4:

	.byte 4,4,4
	.asciz "float"
LDIE_R8:

	.byte 4,8,4
	.asciz "double"
LDIE_BOOLEAN:

	.byte 4,1,2
	.asciz "boolean"
LDIE_CHAR:

	.byte 4,2,8
	.asciz "char"
LDIE_STRING:

	.byte 4,4,1
	.asciz "string"
LDIE_OBJECT:

	.byte 4,4,1
	.asciz "object"
LDIE_SZARRAY:

	.byte 4,4,1
	.asciz "object"
.section __DWARF, __debug_loc,regular,debug
Ldebug_loc_start:
.section __DWARF, __debug_line,regular,debug
Ldebug_line_section_start:
.section __DWARF, __debug_line,regular,debug
Ldebug_line_start:

	.long Ldebug_line_end - . -4
	.short 2
	.long Ldebug_line_header_end - . -4
	.byte 1,1,251,14,13,0,1,1,1,1,0,0,0,1,0,0,1
.section __DWARF, __debug_line,regular,debug
.section __DWARF, __debug_line,regular,debug

	.byte 0
.section __DWARF, __debug_line,regular,debug
	.asciz "xdb.il"

	.byte 0,0,0
.section __DWARF, __debug_line,regular,debug
.section __DWARF, __debug_line,regular,debug

	.byte 0
Ldebug_line_header_end:
.section __DWARF, __debug_line,regular,debug

	.byte 0,1,1
Ldebug_line_end:
.section __DWARF, __debug_frame,regular,debug
	.align 3

	.long Lcie0_end - Lcie0_start
Lcie0_start:

	.long -1
	.byte 3
	.asciz ""

	.byte 1,124,14
	.align 2
Lcie0_end:
.text
	.align 3
methods:
	.space 16
	.align 2
Lm_0:
UISample__ctor:

	.byte 13,192,160,225,128,64,45,233,13,112,160,225,0,89,45,233,72,208,77,226,13,176,160,225,56,0,139,229,0,0,159,229
	.byte 0,0,0,234
	.long mono_aot_Assembly_CSharp_got - . -4
	.byte 0,0,159,231,16,0,139,229,16,224,155,229,0,224,158,229,20,224,139,229,16,224,155,229,68,224,142,226,0,0,160,225
	.byte 0,0,160,225,0,224,158,229,0,0,94,227,0,224,158,21,20,224,155,229,0,224,158,229,16,224,155,229,104,224,142,226
	.byte 0,0,160,225,0,0,160,225,0,224,158,229,0,0,94,227,0,224,158,21,56,0,155,229,64,0,139,229,0,90,159,237
	.byte 0,0,0,234,0,0,0,0,197,90,183,238,0,74,159,237,0,0,0,234,0,0,0,0,196,74,183,238,0,58,159,237
	.byte 0,0,0,234,0,0,128,63,195,58,183,238,0,42,159,237,0,0,0,234,0,0,128,63,194,42,183,238,24,0,139,226
	.byte 0,0,160,227,0,0,160,227,24,0,139,229,0,0,160,227,28,0,139,229,0,0,160,227,32,0,139,229,0,0,160,227
	.byte 36,0,139,229,24,0,139,226,197,11,183,238,2,10,13,237,8,16,29,229,196,11,183,238,2,10,13,237,8,32,29,229
	.byte 195,11,183,238,2,10,13,237,8,48,29,229,194,11,183,238,0,10,141,237
bl p_1

	.byte 64,0,155,229,24,16,139,226,40,16,139,226,24,16,155,229,40,16,139,229,28,16,155,229,44,16,139,229,32,16,155,229
	.byte 48,16,139,229,36,16,155,229,52,16,139,229,0,0,80,227,40,0,0,11,40,16,139,226,20,0,128,226,40,16,155,229
	.byte 0,16,128,229,44,16,155,229,4,16,128,229,48,16,155,229,8,16,128,229,52,16,155,229,12,16,128,229,20,224,155,229
	.byte 0,224,158,229,16,224,155,229,140,224,142,226,1,236,142,226,0,0,160,225,0,224,158,229,0,0,94,227,0,224,158,21
	.byte 56,0,155,229
bl p_2

	.byte 20,224,155,229,0,224,158,229,16,224,155,229,184,224,142,226,1,236,142,226,0,0,160,225,0,224,158,229,0,0,94,227
	.byte 0,224,158,21,16,224,155,229,212,224,142,226,1,236,142,226,0,0,160,225,0,224,158,229,0,0,94,227,0,224,158,21
	.byte 72,208,139,226,0,9,189,232,8,112,157,229,0,160,157,232,14,16,160,225,0,0,159,229
bl p_3

	.byte 148,6,0,2

Lme_0:
	.align 2
Lm_1:
UISample_ViewSetup:

	.byte 13,192,160,225,128,64,45,233,13,112,160,225,112,93,45,233,24,208,77,226,13,176,160,225,0,160,160,225,0,0,159,229
	.byte 0,0,0,234
	.long mono_aot_Assembly_CSharp_got - .
	.byte 0,0,159,231,8,0,139,229,8,224,155,229,0,224,158,229,12,224,139,229,0,96,160,227,0,80,160,227,0,64,160,227
	.byte 8,224,155,229,80,224,142,226,0,0,160,225,0,0,160,225,0,224,158,229,0,0,94,227,0,224,158,21,12,224,155,229
	.byte 0,224,158,229,8,224,155,229,116,224,142,226,0,0,160,225,0,0,160,225,0,224,158,229,0,0,94,227,0,224,158,21
	.byte 10,0,160,225
bl p_4

	.byte 0,16,160,225,0,0,81,227,224,0,0,11,0,16,145,229,15,224,160,225,72,240,145,229,0,0,90,227,219,0,0,11
	.byte 16,0,138,229,12,224,155,229,0,224,158,229,8,224,155,229,196,224,142,226,0,0,160,225,0,0,160,225,0,224,158,229
	.byte 0,0,94,227,0,224,158,21
bl p_5

	.byte 16,0,139,229,10,0,160,225,0,0,159,229,0,0,0,234
	.long mono_aot_Assembly_CSharp_got - . + 4
	.byte 0,0,159,231
bl p_6

	.byte 0,16,160,225,16,32,155,229,16,160,129,229,0,0,159,229,0,0,0,234
	.long mono_aot_Assembly_CSharp_got - . + 8
	.byte 0,0,159,231,20,0,129,229,0,0,159,229,0,0,0,234
	.long mono_aot_Assembly_CSharp_got - . + 12
	.byte 0,0,159,231,12,0,129,229,2,0,160,225,0,0,82,227,185,0,0,11,0,224,146,229
bl p_7

	.byte 12,224,155,229,0,224,158,229,8,224,155,229,80,224,142,226,1,236,142,226,0,0,160,225,0,224,158,229,0,0,94,227
	.byte 0,224,158,21,10,0,160,225,0,0,90,227,171,0,0,11,16,0,154,229,0,0,80,227,168,0,0,11,12,0,144,229
	.byte 0,0,80,227,13,0,0,26,12,224,155,229,0,224,158,229,8,224,155,229,152,224,142,226,1,236,142,226,0,0,160,225
	.byte 0,224,158,229,0,0,94,227,0,224,158,21,0,0,159,229,0,0,0,234
	.long mono_aot_Assembly_CSharp_got - . + 16
	.byte 0,0,159,231
bl p_8

	.byte 12,224,155,229,0,224,158,229,8,224,155,229,208,224,142,226,1,236,142,226,0,0,160,225,0,224,158,229,0,0,94,227
	.byte 0,224,158,21,10,0,160,225,0,0,90,227,139,0,0,11,16,0,154,229,0,80,160,225,12,224,155,229,0,224,158,229
	.byte 8,224,155,229,8,224,142,226,2,236,142,226,0,0,160,225,0,224,158,229,0,0,94,227,0,224,158,21,0,64,160,227
	.byte 12,224,155,229,0,224,158,229,8,224,155,229,48,224,142,226,2,236,142,226,0,0,160,225,0,224,158,229,0,0,94,227
	.byte 0,224,158,21,81,0,0,234,12,224,155,229,0,224,158,229,8,224,155,229,88,224,142,226,2,236,142,226,0,0,160,225
	.byte 0,224,158,229,0,0,94,227,0,224,158,21,5,0,160,225,4,0,160,225,0,0,85,227,104,0,0,11,12,0,149,229
	.byte 4,0,80,225,105,0,0,155,4,1,160,225,0,0,133,224,16,0,128,226,0,0,144,229,0,96,160,225,12,224,155,229
	.byte 0,224,158,229,8,224,155,229,172,224,142,226,2,236,142,226,0,0,160,225,0,224,158,229,0,0,94,227,0,224,158,21
	.byte 6,0,160,225,10,0,160,225,0,0,90,227,83,0,0,11,20,0,138,226,0,0,80,227,80,0,0,11,0,10,144,237
	.byte 192,74,183,238,10,0,160,225,0,0,90,227,75,0,0,11,20,0,138,226,0,0,80,227,72,0,0,11,1,10,144,237
	.byte 192,58,183,238,10,0,160,225,0,0,90,227,67,0,0,11,20,0,138,226,0,0,80,227,64,0,0,11,2,10,144,237
	.byte 192,42,183,238,6,0,160,225,196,11,183,238,2,10,13,237,8,16,29,229,195,11,183,238,2,10,13,237,8,32,29,229
	.byte 194,11,183,238,2,10,13,237,8,48,29,229,0,0,86,227,50,0,0,11,0,192,150,229,15,224,160,225,76,240,156,229
	.byte 12,224,155,229,0,224,158,229,8,224,155,229,112,224,142,226,3,236,142,226,0,0,160,225,0,224,158,229,0,0,94,227
	.byte 0,224,158,21,4,0,160,225,1,0,132,226,0,64,160,225,12,224,155,229,0,224,158,229,8,224,155,229,160,224,142,226
	.byte 3,236,142,226,0,0,160,225,0,224,158,229,0,0,94,227,0,224,158,21,4,0,160,225,5,0,160,225,0,0,85,227
	.byte 22,0,0,11,12,0,149,229,0,0,84,225,157,255,255,186,12,224,155,229,0,224,158,229,8,224,155,229,224,224,142,226
	.byte 3,236,142,226,0,0,160,225,0,224,158,229,0,0,94,227,0,224,158,21,8,224,155,229,252,224,142,226,3,236,142,226
	.byte 0,0,160,225,0,224,158,229,0,0,94,227,0,224,158,21,24,208,139,226,112,13,189,232,8,112,157,229,0,160,157,232
	.byte 14,16,160,225,0,0,159,229
bl p_3

	.byte 148,6,0,2,14,16,160,225,0,0,159,229
bl p_3

	.byte 118,6,0,2

Lme_1:
	.align 2
Lm_2:
UISample_Start:

	.byte 13,192,160,225,128,64,45,233,13,112,160,225,0,89,45,233,16,208,77,226,13,176,160,225,8,0,139,229,0,0,159,229
	.byte 0,0,0,234
	.long mono_aot_Assembly_CSharp_got - . + 20
	.byte 0,0,159,231,0,0,139,229,0,224,155,229,0,224,158,229,4,224,139,229,0,224,155,229,68,224,142,226,0,0,160,225
	.byte 0,0,160,225,0,224,158,229,0,0,94,227,0,224,158,21,4,224,155,229,0,224,158,229,0,224,155,229,104,224,142,226
	.byte 0,0,160,225,0,0,160,225,0,224,158,229,0,0,94,227,0,224,158,21,8,0,155,229
bl p_9

	.byte 4,224,155,229,0,224,158,229,0,224,155,229,148,224,142,226,0,0,160,225,0,0,160,225,0,224,158,229,0,0,94,227
	.byte 0,224,158,21,0,224,155,229,176,224,142,226,0,0,160,225,0,0,160,225,0,224,158,229,0,0,94,227,0,224,158,21
	.byte 16,208,139,226,0,9,189,232,8,112,157,229,0,160,157,232

Lme_2:
	.align 2
Lm_3:
UISample_OnApplicationPause_bool:

	.byte 13,192,160,225,128,64,45,233,13,112,160,225,0,89,45,233,24,208,77,226,13,176,160,225,8,0,139,229,12,16,203,229
	.byte 0,0,159,229,0,0,0,234
	.long mono_aot_Assembly_CSharp_got - . + 24
	.byte 0,0,159,231,0,0,139,229,0,224,155,229,0,224,158,229,4,224,139,229,0,224,155,229,72,224,142,226,0,0,160,225
	.byte 0,0,160,225,0,224,158,229,0,0,94,227,0,224,158,21,4,224,155,229,0,224,158,229,0,224,155,229,108,224,142,226
	.byte 0,0,160,225,0,0,160,225,0,224,158,229,0,0,94,227,0,224,158,21,12,0,219,229,0,0,80,227,62,0,0,10
	.byte 4,224,155,229,0,224,158,229,0,224,155,229,156,224,142,226,0,0,160,225,0,0,160,225,0,224,158,229,0,0,94,227
	.byte 0,224,158,21
bl p_5

	.byte 20,0,139,229,8,0,155,229,16,0,139,229,0,0,159,229,0,0,0,234
	.long mono_aot_Assembly_CSharp_got - . + 4
	.byte 0,0,159,231
bl p_6

	.byte 0,16,160,225,16,0,155,229,20,32,155,229,16,0,129,229,0,0,159,229,0,0,0,234
	.long mono_aot_Assembly_CSharp_got - . + 8
	.byte 0,0,159,231,20,0,129,229,0,0,159,229,0,0,0,234
	.long mono_aot_Assembly_CSharp_got - . + 12
	.byte 0,0,159,231,12,0,129,229,2,0,160,225,0,0,82,227,58,0,0,11,0,224,146,229
bl p_10

	.byte 4,224,155,229,0,224,158,229,0,224,155,229,48,224,142,226,1,236,142,226,0,0,160,225,0,224,158,229,0,0,94,227
	.byte 0,224,158,21
bl p_4

	.byte 0,16,160,225,0,0,81,227,43,0,0,11,0,16,145,229,15,224,160,225,68,240,145,229,4,224,155,229,0,224,158,229
	.byte 0,224,155,229,112,224,142,226,1,236,142,226,0,0,160,225,0,224,158,229,0,0,94,227,0,224,158,21,10,0,0,234
	.byte 4,224,155,229,0,224,158,229,0,224,155,229,152,224,142,226,1,236,142,226,0,0,160,225,0,224,158,229,0,0,94,227
	.byte 0,224,158,21,8,0,155,229
bl p_9

	.byte 4,224,155,229,0,224,158,229,0,224,155,229,196,224,142,226,1,236,142,226,0,0,160,225,0,224,158,229,0,0,94,227
	.byte 0,224,158,21,0,224,155,229,224,224,142,226,1,236,142,226,0,0,160,225,0,224,158,229,0,0,94,227,0,224,158,21
	.byte 24,208,139,226,0,9,189,232,8,112,157,229,0,160,157,232,14,16,160,225,0,0,159,229
bl p_3

	.byte 148,6,0,2

Lme_3:
	.align 2
Lm_4:
UISample_Update:

	.byte 13,192,160,225,128,64,45,233,13,112,160,225,0,89,45,233,16,208,77,226,13,176,160,225,8,0,139,229,0,0,159,229
	.byte 0,0,0,234
	.long mono_aot_Assembly_CSharp_got - . + 28
	.byte 0,0,159,231,0,0,139,229,0,224,155,229,0,224,158,229,4,224,139,229,0,224,155,229,68,224,142,226,0,0,160,225
	.byte 0,0,160,225,0,224,158,229,0,0,94,227,0,224,158,21,4,224,155,229,0,224,158,229,0,224,155,229,104,224,142,226
	.byte 0,0,160,225,0,0,160,225,0,224,158,229,0,0,94,227,0,224,158,21,0,224,155,229,132,224,142,226,0,0,160,225
	.byte 0,0,160,225,0,224,158,229,0,0,94,227,0,224,158,21,16,208,139,226,0,9,189,232,8,112,157,229,0,160,157,232

Lme_4:
	.align 2
Lm_5:
UISample_ReceiveNotificationMessage_object_SpheroDeviceMessenger_MessengerEventArgs:

	.byte 13,192,160,225,128,64,45,233,13,112,160,225,112,89,45,233,36,208,77,226,13,176,160,225,8,0,139,229,12,16,139,229
	.byte 16,32,139,229,0,0,159,229,0,0,0,234
	.long mono_aot_Assembly_CSharp_got - . + 32
	.byte 0,0,159,231,0,0,139,229,0,224,155,229,0,224,158,229,4,224,139,229,0,80,160,227,0,64,160,227,0,224,155,229
	.byte 84,224,142,226,0,0,160,225,0,0,160,225,0,224,158,229,0,0,94,227,0,224,158,21,4,224,155,229,0,224,158,229
	.byte 0,224,155,229,120,224,142,226,0,0,160,225,0,0,160,225,0,224,158,229,0,0,94,227,0,224,158,21,16,16,155,229
	.byte 1,0,160,225,0,0,81,227,151,0,0,11,0,224,145,229
bl p_11

	.byte 0,96,160,225,0,0,86,227,9,0,0,10,0,0,150,229,0,0,144,229,8,0,144,229,8,0,144,229,0,16,159,229
	.byte 0,0,0,234
	.long mono_aot_Assembly_CSharp_got - . + 36
	.byte 1,16,159,231,1,0,80,225,140,0,0,27,6,80,160,225,4,224,155,229,0,224,158,229,0,224,155,229,236,224,142,226
	.byte 0,0,160,225,0,0,160,225,0,224,158,229,0,0,94,227,0,224,158,21
bl p_4

	.byte 24,0,139,229,6,0,160,225,6,0,160,225,0,0,86,227,120,0,0,11,0,224,150,229
bl p_12

	.byte 0,16,160,225,24,32,155,229,2,0,160,225,0,0,82,227,113,0,0,11,0,32,146,229,15,224,160,225,52,240,146,229
	.byte 0,64,160,225,4,224,155,229,0,224,158,229,0,224,155,229,84,224,142,226,1,236,142,226,0,0,160,225,0,224,158,229
	.byte 0,0,94,227,0,224,158,21,6,0,160,225,6,0,160,225,0,0,86,227,96,0,0,11,0,224,150,229
bl p_13

	.byte 1,16,160,227,1,0,80,227,71,0,0,26,4,224,155,229,0,224,158,229,0,224,155,229,156,224,142,226,1,236,142,226
	.byte 0,0,160,225,0,224,158,229,0,0,94,227,0,224,158,21,4,0,160,225,1,0,160,227,4,0,160,225,1,16,160,227
	.byte 0,0,84,227,76,0,0,11,0,224,148,229
bl p_14

	.byte 4,224,155,229,0,224,158,229,0,224,155,229,224,224,142,226,1,236,142,226,0,0,160,225,0,224,158,229,0,0,94,227
	.byte 0,224,158,21,8,0,155,229,24,0,139,229
bl p_4

	.byte 0,16,160,225,0,0,81,227,59,0,0,11,0,16,145,229,15,224,160,225,72,240,145,229,0,16,160,225,24,0,155,229
	.byte 0,0,80,227,52,0,0,11,16,16,128,229,4,224,155,229,0,224,158,229,0,224,155,229,60,224,142,226,2,236,142,226
	.byte 0,0,160,225,0,224,158,229,0,0,94,227,0,224,158,21,8,0,155,229,0,0,80,227,39,0,0,11,16,0,144,229
	.byte 0,0,80,227,36,0,0,11,12,0,144,229,0,0,80,227,13,0,0,26,4,224,155,229,0,224,158,229,0,224,155,229
	.byte 132,224,142,226,2,236,142,226,0,0,160,225,0,224,158,229,0,0,94,227,0,224,158,21,0,0,159,229,0,0,0,234
	.long mono_aot_Assembly_CSharp_got - . + 40
	.byte 0,0,159,231
bl p_8

	.byte 4,224,155,229,0,224,158,229,0,224,155,229,188,224,142,226,2,236,142,226,0,0,160,225,0,224,158,229,0,0,94,227
	.byte 0,224,158,21,0,224,155,229,216,224,142,226,2,236,142,226,0,0,160,225,0,224,158,229,0,0,94,227,0,224,158,21
	.byte 36,208,139,226,112,9,189,232,8,112,157,229,0,160,157,232,14,16,160,225,0,0,159,229
bl p_3

	.byte 148,6,0,2,14,16,160,225,0,0,159,229
bl p_3

	.byte 120,6,0,2

Lme_5:
	.align 2
Lm_7:
wrapper_managed_to_native_System_Array_GetGenericValueImpl_int_object_:

	.byte 13,192,160,225,240,95,45,233,128,208,77,226,13,176,160,225,8,0,139,229,12,16,139,229,16,32,139,229
bl p_15

	.byte 24,16,141,226,4,0,129,229,0,32,144,229,0,32,129,229,0,16,128,229,16,208,129,229,15,32,160,225,20,32,129,229
	.byte 0,0,159,229,0,0,0,234
	.long mono_aot_Assembly_CSharp_got - . + 44
	.byte 0,0,159,231,0,0,139,229,0,224,155,229,0,224,158,229,4,224,139,229,0,224,155,229,104,224,142,226,0,0,160,225
	.byte 0,0,160,225,0,224,158,229,0,0,94,227,0,224,158,21,4,224,155,229,0,224,158,229,0,224,155,229,140,224,142,226
	.byte 0,0,160,225,0,0,160,225,0,224,158,229,0,0,94,227,0,224,158,21,8,0,155,229,0,0,80,227,16,0,0,26
	.byte 4,224,155,229,0,224,158,229,0,224,155,229,188,224,142,226,0,0,160,225,0,0,160,225,0,224,158,229,0,0,94,227
	.byte 0,224,158,21,148,0,160,227,6,12,128,226,2,4,128,226,148,0,160,227,6,12,128,226,2,4,128,226
bl p_16
bl p_17

	.byte 4,224,155,229,0,224,158,229,0,224,155,229,0,224,142,226,1,236,142,226,0,0,160,225,0,224,158,229,0,0,94,227
	.byte 0,224,158,21,8,0,155,229,12,16,155,229,16,32,155,229
bl p_18

	.byte 4,224,155,229,0,224,158,229,0,224,155,229,52,224,142,226,1,236,142,226,0,0,160,225,0,224,158,229,0,0,94,227
	.byte 0,224,158,21,0,0,159,229,0,0,0,234
	.long mono_aot_Assembly_CSharp_got - . + 48
	.byte 0,0,159,231,0,0,144,229,0,0,80,227,18,0,0,10,4,224,155,229,0,224,158,229,0,224,155,229,116,224,142,226
	.byte 1,236,142,226,0,0,160,225,0,224,158,229,0,0,94,227,0,224,158,21,4,224,155,229,0,224,158,229,0,224,155,229
	.byte 152,224,142,226,1,236,142,226,0,0,160,225,0,224,158,229,0,0,94,227,0,224,158,21
bl p_19

	.byte 4,224,155,229,0,224,158,229,0,224,155,229,192,224,142,226,1,236,142,226,0,0,160,225,0,224,158,229,0,0,94,227
	.byte 0,224,158,21,0,224,155,229,220,224,142,226,1,236,142,226,0,0,160,225,0,224,158,229,0,0,94,227,0,224,158,21
	.byte 24,32,139,226,0,192,146,229,4,224,146,229,0,192,142,229,104,208,130,226,240,175,157,232

Lme_7:
.text
	.align 3
methods_end:
.text
	.align 3
method_offsets:

	.long Lm_0 - methods,Lm_1 - methods,Lm_2 - methods,Lm_3 - methods,Lm_4 - methods,Lm_5 - methods,-1,Lm_7 - methods

.text
	.align 3
method_info:
mi:
Lm_0_p:

	.byte 0,1,2
Lm_1_p:

	.byte 0,5,3,4,5,6,7
Lm_2_p:

	.byte 0,1,8
Lm_3_p:

	.byte 0,4,9,4,5,6
Lm_4_p:

	.byte 0,1,10
Lm_5_p:

	.byte 0,3,11,12,13
Lm_7_p:

	.byte 0,2,14,15
.text
	.align 3
method_info_offsets:

	.long Lm_0_p - mi,Lm_1_p - mi,Lm_2_p - mi,Lm_3_p - mi,Lm_4_p - mi,Lm_5_p - mi,0,Lm_7_p - mi

.text
	.align 3
extra_method_info:

	.byte 0,1,6,83,121,115,116,101,109,46,65,114,114,97,121,58,71,101,116,71,101,110,101,114,105,99,86,97,108,117,101,73
	.byte 109,112,108,32,40,105,110,116,44,111,98,106,101,99,116,38,41,0

.text
	.align 3
extra_method_table:

	.long 11,0,0,0,1,7,0,0
	.long 0,0,0,0,0,0,0,0
	.long 0,0,0,0,0,0,0,0
	.long 0,0,0,0,0,0,0,0
	.long 0,0
.text
	.align 3
extra_method_info_offsets:

	.long 1,7,1
.text
	.align 3
method_order:

	.long 0,16777215,0,1,2,3,4,5
	.long 7

.text
method_order_end:
.text
	.align 3
class_name_table:

	.short 11, 1, 0, 0, 0, 0, 0, 0
	.short 0, 0, 0, 0, 0, 0, 0, 2
	.short 0, 0, 0, 0, 0, 0, 0
.text
	.align 3
got_info:

	.byte 12,0,39,40,40,14,46,1,6,6,30,46,1,17,0,1,40,40,40,40,11,31,1,17,0,45,40,33,3,194,0,11
	.byte 136,3,194,0,18,194,7,32,109,111,110,111,95,97,114,99,104,95,116,104,114,111,119,95,99,111,114,108,105,98,95,101
	.byte 120,99,101,112,116,105,111,110,0,3,193,0,0,202,3,193,0,0,152,7,20,109,111,110,111,95,111,98,106,101,99,116
	.byte 95,110,101,119,95,102,97,115,116,0,3,255,252,0,0,0,193,0,0,150,3,194,0,17,241,3,2,3,255,252,0,0
	.byte 0,193,0,0,151,3,193,0,0,159,3,193,0,0,132,3,193,0,0,161,3,193,0,0,86,7,17,109,111,110,111,95
	.byte 103,101,116,95,108,109,102,95,97,100,100,114,0,7,30,109,111,110,111,95,99,114,101,97,116,101,95,99,111,114,108,105
	.byte 98,95,101,120,99,101,112,116,105,111,110,95,48,0,7,25,109,111,110,111,95,97,114,99,104,95,116,104,114,111,119,95
	.byte 101,120,99,101,112,116,105,111,110,0,31,255,254,0,0,0,41,3,3,198,0,4,3,0,1,1,2,3,7,35,109,111
	.byte 110,111,95,116,104,114,101,97,100,95,105,110,116,101,114,114,117,112,116,105,111,110,95,99,104,101,99,107,112,111,105,110
	.byte 116,0
.text
	.align 3
got_info_offsets:

	.long 0,2,3,4,5,8,10,13
	.long 16,17,18,19,20,23,26,27
.text
	.align 3
ex_info:
ex:
Le_0_p:

	.byte 130,8,10,0,5,255,255,255,255,255,60,0,1,36,1,2,31,129,36,1,3,6,44,0,192,255,255,218,28,0,51,129
	.byte 232,88,130,8,208,0,0,11,56,20,0,88,1,44,5,16,5,16,5,16,5,56,0,4,0,8,0,4,0,8,0,4
	.byte 0,8,0,4,0,8,5,48,0,4,5,44,1,40,5,4,1,64
Le_1_p:

	.byte 132,64,10,26,14,255,255,255,255,255,72,0,1,36,1,2,16,80,1,3,22,128,140,2,4,5,13,72,1,5,10,56
	.byte 1,6,7,56,1,7,2,40,1,11,5,40,1,9,4,84,1,10,39,128,196,1,11,4,48,2,8,12,9,64,0,192
	.byte 255,255,124,28,0,128,222,132,16,100,132,64,10,6,5,4,106,0,100,0,36,1,4,0,4,5,4,0,4,0,4,0
	.byte 4,5,8,0,4,0,4,5,4,0,36,5,8,1,4,0,16,0,4,0,8,0,4,0,16,0,4,0,16,11,4,0
	.byte 4,0,4,0,4,0,4,0,0,5,4,0,36,1,4,0,4,0,4,7,16,0,4,5,4,0,36,5,16,5,4,0
	.byte 36,1,4,0,4,0,4,5,4,1,4,0,36,2,4,0,36,5,4,0,36,1,4,1,4,0,4,0,4,0,4,0
	.byte 4,0,4,0,4,0,4,0,4,1,4,1,4,0,36,1,4,1,4,0,4,0,4,5,4,0,4,0,4,5,8,1
	.byte 4,0,4,0,4,5,4,0,4,0,4,5,8,1,4,0,4,0,4,5,4,0,4,0,4,5,8,0,4,0,8,0
	.byte 4,0,8,0,4,0,8,0,4,0,4,0,4,0,4,5,8,0,36,2,4,1,4,1,4,0,36,1,4,3,16,0
	.byte 4,5,4,1,64
Le_2_p:

	.byte 128,212,10,60,4,255,255,255,255,255,60,0,1,36,1,2,6,44,0,192,255,255,249,28,0,19,128,196,88,128,212,208
	.byte 0,0,11,8,4,0,88,1,40,5,4,1,64
Le_3_p:

	.byte 130,20,10,86,8,255,255,255,255,255,64,0,1,36,2,2,5,6,48,1,3,22,128,148,1,4,10,64,1,6,5,40
	.byte 1,6,6,44,0,192,255,255,206,28,0,80,129,244,92,130,20,208,0,0,11,12,208,0,0,11,8,32,0,92,1,40
	.byte 0,4,5,4,0,36,6,16,0,16,0,4,0,12,0,4,0,16,0,4,0,16,11,4,0,4,0,4,0,4,0,4
	.byte 0,0,5,4,0,36,0,4,5,4,0,4,0,4,0,4,5,8,0,36,5,4,1,40,5,4,1,64
Le_4_p:

	.byte 128,168,10,60,3,255,255,255,255,255,60,0,1,36,0,192,255,255,255,28,0,15,128,152,88,128,168,208,0,0,11,8
	.byte 2,0,88,1,64
Le_5_p:

	.byte 131,28,10,112,10,255,255,255,255,255,76,0,1,36,1,2,12,116,1,3,17,104,2,4,8,12,72,1,5,7,68,1
	.byte 6,16,92,2,7,8,13,72,1,8,10,56,0,192,255,255,168,28,0,128,177,130,236,104,131,28,208,0,0,11,12,208
	.byte 0,0,11,16,208,0,0,11,8,5,4,77,0,104,1,40,0,4,0,4,0,4,0,4,0,0,0,4,5,4,0,4
	.byte 0,4,0,4,0,4,0,4,0,4,0,16,0,4,5,4,1,4,0,36,5,8,1,4,0,4,0,4,0,4,0,4
	.byte 0,0,0,4,5,8,0,4,0,4,0,4,0,4,5,8,1,4,0,36,1,4,0,4,0,4,0,4,0,4,0,0
	.byte 5,4,1,4,0,4,5,4,0,36,1,4,1,4,0,4,0,4,0,4,0,4,0,4,0,0,5,4,1,44,0,4
	.byte 5,4,0,4,0,4,0,4,0,8,5,8,0,4,0,4,5,4,1,40,0,4,0,4,7,16,0,4,5,4,0,36
	.byte 5,16,5,4,1,64
Le_7_p:

	.byte 130,8,10,128,144,9,255,255,255,255,255,96,0,1,36,2,2,3,6,48,0,6,68,1,4,14,52,2,5,7,12,64
	.byte 1,6,2,36,1,7,6,40,0,192,255,255,209,28,0,63,129,240,124,130,8,208,0,0,11,12,208,0,0,11,16,208
	.byte 0,0,11,8,21,0,124,1,40,0,4,5,4,0,36,0,12,0,12,5,4,0,4,1,0,9,48,5,4,0,36,6
	.byte 16,1,4,0,4,5,4,2,36,0,36,6,4,1,64
.text
	.align 3
ex_info_offsets:

	.long Le_0_p - ex,Le_1_p - ex,Le_2_p - ex,Le_3_p - ex,Le_4_p - ex,Le_5_p - ex,0,Le_7_p - ex

.text
	.align 3
unwind_info:

	.byte 25,12,13,0,76,14,8,135,2,68,14,24,136,6,139,5,140,4,142,3,68,14,96,68,13,11,33,12,13,0,76,14
	.byte 8,135,2,68,14,40,132,10,133,9,134,8,136,7,138,6,139,5,140,4,142,3,68,14,64,68,13,11,25,12,13,0
	.byte 76,14,8,135,2,68,14,24,136,6,139,5,140,4,142,3,68,14,40,68,13,11,25,12,13,0,76,14,8,135,2,68
	.byte 14,24,136,6,139,5,140,4,142,3,68,14,48,68,13,11,31,12,13,0,76,14,8,135,2,68,14,36,132,9,133,8
	.byte 134,7,136,6,139,5,140,4,142,3,68,14,72,68,13,11,33,12,13,0,72,14,40,132,10,133,9,134,8,135,7,136
	.byte 6,137,5,138,4,139,3,140,2,142,1,68,14,168,1,68,13,11
.text
	.align 3
class_info:
LK_I_0:

	.byte 0,128,144,8,0,0,1
LK_I_1:

	.byte 4,128,160,36,0,0,4,194,0,19,86,194,0,19,61,195,0,0,4,194,0,19,60
.text
	.align 3
class_info_offsets:

	.long LK_I_0 - class_info,LK_I_1 - class_info


.text
	.align 4
plt:
mono_aot_Assembly_CSharp_plt:

	.byte 0,192,159,229,12,240,159,231
	.long mono_aot_Assembly_CSharp_got - . + 60,0
p_1:
plt_UnityEngine_Color__ctor_single_single_single_single:

	.byte 0,192,159,229,12,240,159,231
	.long mono_aot_Assembly_CSharp_got - . + 64,28
p_2:
plt_UnityEngine_MonoBehaviour__ctor:

	.byte 0,192,159,229,12,240,159,231
	.long mono_aot_Assembly_CSharp_got - . + 68,33
p_3:
plt__jit_icall_mono_arch_throw_corlib_exception:

	.byte 0,192,159,229,12,240,159,231
	.long mono_aot_Assembly_CSharp_got - . + 72,38
p_4:
plt_SpheroProvider_GetSharedProvider:

	.byte 0,192,159,229,12,240,159,231
	.long mono_aot_Assembly_CSharp_got - . + 76,73
p_5:
plt_SpheroDeviceMessenger_get_SharedInstance:

	.byte 0,192,159,229,12,240,159,231
	.long mono_aot_Assembly_CSharp_got - . + 80,78
p_6:
plt__jit_icall_mono_object_new_fast:

	.byte 0,192,159,229,12,240,159,231
	.long mono_aot_Assembly_CSharp_got - . + 84,83
p_7:
plt_SpheroDeviceMessenger_add_NotificationReceived_SpheroDeviceMessenger_MessengerEventHandler:

	.byte 0,192,159,229,12,240,159,231
	.long mono_aot_Assembly_CSharp_got - . + 88,106
p_8:
plt_UnityEngine_Application_LoadLevel_string:

	.byte 0,192,159,229,12,240,159,231
	.long mono_aot_Assembly_CSharp_got - . + 92,116
p_9:
plt_UISample_ViewSetup:

	.byte 0,192,159,229,12,240,159,231
	.long mono_aot_Assembly_CSharp_got - . + 96,121
p_10:
plt_SpheroDeviceMessenger_remove_NotificationReceived_SpheroDeviceMessenger_MessengerEventHandler:

	.byte 0,192,159,229,12,240,159,231
	.long mono_aot_Assembly_CSharp_got - . + 100,123
p_11:
plt_SpheroDeviceMessenger_MessengerEventArgs_get_Message:

	.byte 0,192,159,229,12,240,159,231
	.long mono_aot_Assembly_CSharp_got - . + 104,133
p_12:
plt_SpheroDeviceMessage_get_RobotID:

	.byte 0,192,159,229,12,240,159,231
	.long mono_aot_Assembly_CSharp_got - . + 108,138
p_13:
plt_SpheroDeviceNotification_get_NotificationType:

	.byte 0,192,159,229,12,240,159,231
	.long mono_aot_Assembly_CSharp_got - . + 112,143
p_14:
plt_Sphero_set_ConnectionState_Sphero_Connection_State:

	.byte 0,192,159,229,12,240,159,231
	.long mono_aot_Assembly_CSharp_got - . + 116,148
p_15:
plt__jit_icall_mono_get_lmf_addr:

	.byte 0,192,159,229,12,240,159,231
	.long mono_aot_Assembly_CSharp_got - . + 120,153
p_16:
plt__jit_icall_mono_create_corlib_exception_0:

	.byte 0,192,159,229,12,240,159,231
	.long mono_aot_Assembly_CSharp_got - . + 124,173
p_17:
plt__jit_icall_mono_arch_throw_exception:

	.byte 0,192,159,229,12,240,159,231
	.long mono_aot_Assembly_CSharp_got - . + 128,206
p_18:
plt__icall_native_System_Array_GetGenericValueImpl_object_int_object_:

	.byte 0,192,159,229,12,240,159,231
	.long mono_aot_Assembly_CSharp_got - . + 132,234
p_19:
plt__jit_icall_mono_thread_interruption_checkpoint:

	.byte 0,192,159,229,12,240,159,231
	.long mono_aot_Assembly_CSharp_got - . + 136,252
plt_end:
.text
	.align 3
mono_image_table:

	.long 4
	.asciz "Assembly-CSharp"
	.asciz "73551DC4-DBB4-4FDE-8AF4-E74EEE6DA807"
	.asciz ""
	.asciz ""
	.align 3

	.long 0,0,0,0,0
	.asciz "Assembly-CSharp-firstpass"
	.asciz "097540E6-7BD8-4FD0-A495-A04603854727"
	.asciz ""
	.asciz ""
	.align 3

	.long 0,1,0,0,0
	.asciz "UnityEngine"
	.asciz "916A2883-C8D8-4535-9CFF-3D01B84826C3"
	.asciz ""
	.asciz ""
	.align 3

	.long 0,0,0,0,0
	.asciz "mscorlib"
	.asciz "45DDC7DB-3693-4957-9938-935D77B10F93"
	.asciz ""
	.asciz "7cec85d7bea7798e"
	.align 3

	.long 1,2,0,5,0
.data
	.align 3
mono_aot_Assembly_CSharp_got:
	.space 144
got_end:
.data
	.align 3
mono_aot_got_addr:
	.align 2
	.long mono_aot_Assembly_CSharp_got
.data
	.align 3
mono_aot_file_info:

	.long 16,144,20,8,1024,1024,128,0
	.long 0,0,0,0,0
.text
	.align 2
mono_assembly_guid:
	.asciz "73551DC4-DBB4-4FDE-8AF4-E74EEE6DA807"
.text
	.align 2
mono_aot_version:
	.asciz "66"
.text
	.align 2
mono_aot_opt_flags:
	.asciz "55650815"
.text
	.align 2
mono_aot_full_aot:
	.asciz "TRUE"
.text
	.align 2
mono_runtime_version:
	.asciz ""
.text
	.align 2
mono_aot_assembly_name:
	.asciz "Assembly-CSharp"
.text
	.align 3
Lglobals_hash:

	.short 73, 26, 0, 0, 0, 0, 0, 0
	.short 0, 14, 0, 18, 0, 0, 0, 0
	.short 0, 5, 0, 0, 0, 0, 0, 0
	.short 0, 0, 0, 0, 0, 0, 0, 28
	.short 0, 12, 0, 4, 0, 0, 0, 0
	.short 0, 3, 0, 27, 0, 0, 0, 8
	.short 0, 0, 0, 0, 0, 0, 0, 13
	.short 0, 1, 0, 0, 0, 0, 0, 11
	.short 74, 0, 0, 0, 0, 0, 0, 29
	.short 0, 2, 75, 0, 0, 0, 0, 0
	.short 0, 0, 0, 0, 0, 0, 0, 0
	.short 0, 21, 0, 0, 0, 0, 0, 0
	.short 0, 10, 0, 16, 0, 7, 0, 0
	.short 0, 0, 0, 0, 0, 0, 0, 0
	.short 0, 0, 0, 0, 0, 0, 0, 0
	.short 0, 0, 0, 0, 0, 15, 0, 19
	.short 0, 6, 73, 23, 0, 9, 0, 0
	.short 0, 0, 0, 0, 0, 0, 0, 0
	.short 0, 20, 0, 17, 76, 22, 0, 24
	.short 0, 25, 0
.text
	.align 2
name_0:
	.asciz "methods"
.text
	.align 2
name_1:
	.asciz "methods_end"
.text
	.align 2
name_2:
	.asciz "method_offsets"
.text
	.align 2
name_3:
	.asciz "method_info"
.text
	.align 2
name_4:
	.asciz "method_info_offsets"
.text
	.align 2
name_5:
	.asciz "extra_method_info"
.text
	.align 2
name_6:
	.asciz "extra_method_table"
.text
	.align 2
name_7:
	.asciz "extra_method_info_offsets"
.text
	.align 2
name_8:
	.asciz "method_order"
.text
	.align 2
name_9:
	.asciz "method_order_end"
.text
	.align 2
name_10:
	.asciz "class_name_table"
.text
	.align 2
name_11:
	.asciz "got_info"
.text
	.align 2
name_12:
	.asciz "got_info_offsets"
.text
	.align 2
name_13:
	.asciz "ex_info"
.text
	.align 2
name_14:
	.asciz "ex_info_offsets"
.text
	.align 2
name_15:
	.asciz "unwind_info"
.text
	.align 2
name_16:
	.asciz "class_info"
.text
	.align 2
name_17:
	.asciz "class_info_offsets"
.text
	.align 2
name_18:
	.asciz "plt"
.text
	.align 2
name_19:
	.asciz "plt_end"
.text
	.align 2
name_20:
	.asciz "mono_image_table"
.text
	.align 2
name_21:
	.asciz "mono_aot_got_addr"
.text
	.align 2
name_22:
	.asciz "mono_aot_file_info"
.text
	.align 2
name_23:
	.asciz "mono_assembly_guid"
.text
	.align 2
name_24:
	.asciz "mono_aot_version"
.text
	.align 2
name_25:
	.asciz "mono_aot_opt_flags"
.text
	.align 2
name_26:
	.asciz "mono_aot_full_aot"
.text
	.align 2
name_27:
	.asciz "mono_runtime_version"
.text
	.align 2
name_28:
	.asciz "mono_aot_assembly_name"
.data
	.align 3
Lglobals:
	.align 2
	.long Lglobals_hash
	.align 2
	.long name_0
	.align 2
	.long methods
	.align 2
	.long name_1
	.align 2
	.long methods_end
	.align 2
	.long name_2
	.align 2
	.long method_offsets
	.align 2
	.long name_3
	.align 2
	.long method_info
	.align 2
	.long name_4
	.align 2
	.long method_info_offsets
	.align 2
	.long name_5
	.align 2
	.long extra_method_info
	.align 2
	.long name_6
	.align 2
	.long extra_method_table
	.align 2
	.long name_7
	.align 2
	.long extra_method_info_offsets
	.align 2
	.long name_8
	.align 2
	.long method_order
	.align 2
	.long name_9
	.align 2
	.long method_order_end
	.align 2
	.long name_10
	.align 2
	.long class_name_table
	.align 2
	.long name_11
	.align 2
	.long got_info
	.align 2
	.long name_12
	.align 2
	.long got_info_offsets
	.align 2
	.long name_13
	.align 2
	.long ex_info
	.align 2
	.long name_14
	.align 2
	.long ex_info_offsets
	.align 2
	.long name_15
	.align 2
	.long unwind_info
	.align 2
	.long name_16
	.align 2
	.long class_info
	.align 2
	.long name_17
	.align 2
	.long class_info_offsets
	.align 2
	.long name_18
	.align 2
	.long plt
	.align 2
	.long name_19
	.align 2
	.long plt_end
	.align 2
	.long name_20
	.align 2
	.long mono_image_table
	.align 2
	.long name_21
	.align 2
	.long mono_aot_got_addr
	.align 2
	.long name_22
	.align 2
	.long mono_aot_file_info
	.align 2
	.long name_23
	.align 2
	.long mono_assembly_guid
	.align 2
	.long name_24
	.align 2
	.long mono_aot_version
	.align 2
	.long name_25
	.align 2
	.long mono_aot_opt_flags
	.align 2
	.long name_26
	.align 2
	.long mono_aot_full_aot
	.align 2
	.long name_27
	.align 2
	.long mono_runtime_version
	.align 2
	.long name_28
	.align 2
	.long mono_aot_assembly_name

	.long 0,0
	.globl _mono_aot_module_Assembly_CSharp_info
	.align 3
_mono_aot_module_Assembly_CSharp_info:
	.align 2
	.long Lglobals
.section __DWARF, __debug_info,regular,debug
LTDIE_5:

	.byte 17
	.asciz "System_Object"

	.byte 8,7
	.asciz "System_Object"

	.long LTDIE_5 - Ldebug_info_start
LTDIE_5_POINTER:

	.byte 13
	.long LTDIE_5 - Ldebug_info_start
LTDIE_5_REFERENCE:

	.byte 14
	.long LTDIE_5 - Ldebug_info_start
LTDIE_4:

	.byte 5
	.asciz "UnityEngine_Object"

	.byte 16,16
	.long LTDIE_5 - Ldebug_info_start
	.byte 2,35,0,6
	.asciz "m_UnityRuntimeReferenceData"

	.long LDIE_I4 - Ldebug_info_start
	.byte 2,35,8,0,7
	.asciz "UnityEngine_Object"

	.long LTDIE_4 - Ldebug_info_start
LTDIE_4_POINTER:

	.byte 13
	.long LTDIE_4 - Ldebug_info_start
LTDIE_4_REFERENCE:

	.byte 14
	.long LTDIE_4 - Ldebug_info_start
LTDIE_3:

	.byte 5
	.asciz "UnityEngine_Component"

	.byte 16,16
	.long LTDIE_4 - Ldebug_info_start
	.byte 2,35,0,0,7
	.asciz "UnityEngine_Component"

	.long LTDIE_3 - Ldebug_info_start
LTDIE_3_POINTER:

	.byte 13
	.long LTDIE_3 - Ldebug_info_start
LTDIE_3_REFERENCE:

	.byte 14
	.long LTDIE_3 - Ldebug_info_start
LTDIE_2:

	.byte 5
	.asciz "UnityEngine_Behaviour"

	.byte 16,16
	.long LTDIE_3 - Ldebug_info_start
	.byte 2,35,0,0,7
	.asciz "UnityEngine_Behaviour"

	.long LTDIE_2 - Ldebug_info_start
LTDIE_2_POINTER:

	.byte 13
	.long LTDIE_2 - Ldebug_info_start
LTDIE_2_REFERENCE:

	.byte 14
	.long LTDIE_2 - Ldebug_info_start
LTDIE_1:

	.byte 5
	.asciz "UnityEngine_MonoBehaviour"

	.byte 16,16
	.long LTDIE_2 - Ldebug_info_start
	.byte 2,35,0,0,7
	.asciz "UnityEngine_MonoBehaviour"

	.long LTDIE_1 - Ldebug_info_start
LTDIE_1_POINTER:

	.byte 13
	.long LTDIE_1 - Ldebug_info_start
LTDIE_1_REFERENCE:

	.byte 14
	.long LTDIE_1 - Ldebug_info_start
LTDIE_0:

	.byte 5
	.asciz "_UISample"

	.byte 36,16
	.long LTDIE_1 - Ldebug_info_start
	.byte 2,35,0,6
	.asciz "BLUE"

	.long LDIE_I4 - Ldebug_info_start
	.byte 2,35,20,6
	.asciz "m_Spheros"

	.long LDIE_SZARRAY - Ldebug_info_start
	.byte 2,35,16,0,7
	.asciz "_UISample"

	.long LTDIE_0 - Ldebug_info_start
LTDIE_0_POINTER:

	.byte 13
	.long LTDIE_0 - Ldebug_info_start
LTDIE_0_REFERENCE:

	.byte 14
	.long LTDIE_0 - Ldebug_info_start
	.byte 2
	.asciz "UISample:.ctor"
	.long Lm_0
	.long Lme_0

	.byte 2,118,16,3
	.asciz "this"

	.long LTDIE_0_REFERENCE - Ldebug_info_start
	.byte 2,123,56,0

.section __DWARF, __debug_frame,regular,debug

	.long Lfde0_end - Lfde0_start
Lfde0_start:

	.long 0
	.align 2
	.long Lm_0

	.long Lme_0 - Lm_0
	.byte 12,13,0,76,14,8,135,2,68,14,24,136,6,139,5,140,4,142,3,68,14,96,68,13,11
	.align 2
Lfde0_end:

.section __DWARF, __debug_info,regular,debug
LTDIE_7:

	.byte 5
	.asciz "_BluetoothDeviceInfo"

	.byte 16,16
	.long LTDIE_5 - Ldebug_info_start
	.byte 2,35,0,6
	.asciz "m_Name"

	.long LDIE_STRING - Ldebug_info_start
	.byte 2,35,8,6
	.asciz "m_UniqueId"

	.long LDIE_STRING - Ldebug_info_start
	.byte 2,35,12,0,7
	.asciz "_BluetoothDeviceInfo"

	.long LTDIE_7 - Ldebug_info_start
LTDIE_7_POINTER:

	.byte 13
	.long LTDIE_7 - Ldebug_info_start
LTDIE_7_REFERENCE:

	.byte 14
	.long LTDIE_7 - Ldebug_info_start
LTDIE_8:

	.byte 8
	.asciz "_Connection_State"

	.byte 4
	.long LDIE_I4 - Ldebug_info_start
	.byte 9
	.asciz "Failed"

	.byte 0,9
	.asciz "Disconnected"

	.byte 1,9
	.asciz "Connecting"

	.byte 2,9
	.asciz "Connected"

	.byte 3,0,7
	.asciz "_Connection_State"

	.long LTDIE_8 - Ldebug_info_start
LTDIE_8_POINTER:

	.byte 13
	.long LTDIE_8 - Ldebug_info_start
LTDIE_8_REFERENCE:

	.byte 14
	.long LTDIE_8 - Ldebug_info_start
LTDIE_6:

	.byte 5
	.asciz "_Sphero"

	.byte 32,16
	.long LTDIE_5 - Ldebug_info_start
	.byte 2,35,0,6
	.asciz "m_DeviceInfo"

	.long LTDIE_7_REFERENCE - Ldebug_info_start
	.byte 2,35,8,6
	.asciz "m_RGBLEDColor"

	.long LDIE_I4 - Ldebug_info_start
	.byte 2,35,12,6
	.asciz "m_ConnectionState"

	.long LTDIE_8 - Ldebug_info_start
	.byte 2,35,28,0,7
	.asciz "_Sphero"

	.long LTDIE_6 - Ldebug_info_start
LTDIE_6_POINTER:

	.byte 13
	.long LTDIE_6 - Ldebug_info_start
LTDIE_6_REFERENCE:

	.byte 14
	.long LTDIE_6 - Ldebug_info_start
	.byte 2
	.asciz "UISample:ViewSetup"
	.long Lm_1
	.long Lme_1

	.byte 2,118,16,3
	.asciz "this"

	.long LTDIE_0_REFERENCE - Ldebug_info_start
	.byte 1,90,11
	.asciz "sphero"

	.long LTDIE_6_REFERENCE - Ldebug_info_start
	.byte 1,86,11
	.asciz "V_1"

	.long LDIE_SZARRAY - Ldebug_info_start
	.byte 1,85,11
	.asciz "V_2"

	.long LDIE_I4 - Ldebug_info_start
	.byte 1,84,0

.section __DWARF, __debug_frame,regular,debug

	.long Lfde1_end - Lfde1_start
Lfde1_start:

	.long 0
	.align 2
	.long Lm_1

	.long Lme_1 - Lm_1
	.byte 12,13,0,76,14,8,135,2,68,14,40,132,10,133,9,134,8,136,7,138,6,139,5,140,4,142,3,68,14,64,68,13
	.byte 11
	.align 2
Lfde1_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "UISample:Start"
	.long Lm_2
	.long Lme_2

	.byte 2,118,16,3
	.asciz "this"

	.long LTDIE_0_REFERENCE - Ldebug_info_start
	.byte 2,123,8,0

.section __DWARF, __debug_frame,regular,debug

	.long Lfde2_end - Lfde2_start
Lfde2_start:

	.long 0
	.align 2
	.long Lm_2

	.long Lme_2 - Lm_2
	.byte 12,13,0,76,14,8,135,2,68,14,24,136,6,139,5,140,4,142,3,68,14,40,68,13,11
	.align 2
Lfde2_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "UISample:OnApplicationPause"
	.long Lm_3
	.long Lme_3

	.byte 2,118,16,3
	.asciz "this"

	.long LTDIE_0_REFERENCE - Ldebug_info_start
	.byte 2,123,8,3
	.asciz "pause"

	.long LDIE_BOOLEAN - Ldebug_info_start
	.byte 2,123,12,0

.section __DWARF, __debug_frame,regular,debug

	.long Lfde3_end - Lfde3_start
Lfde3_start:

	.long 0
	.align 2
	.long Lm_3

	.long Lme_3 - Lm_3
	.byte 12,13,0,76,14,8,135,2,68,14,24,136,6,139,5,140,4,142,3,68,14,48,68,13,11
	.align 2
Lfde3_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "UISample:Update"
	.long Lm_4
	.long Lme_4

	.byte 2,118,16,3
	.asciz "this"

	.long LTDIE_0_REFERENCE - Ldebug_info_start
	.byte 2,123,8,0

.section __DWARF, __debug_frame,regular,debug

	.long Lfde4_end - Lfde4_start
Lfde4_start:

	.long 0
	.align 2
	.long Lm_4

	.long Lme_4 - Lm_4
	.byte 12,13,0,76,14,8,135,2,68,14,24,136,6,139,5,140,4,142,3,68,14,40,68,13,11
	.align 2
Lfde4_end:

.section __DWARF, __debug_info,regular,debug
LTDIE_10:

	.byte 5
	.asciz "System_EventArgs"

	.byte 8,16
	.long LTDIE_5 - Ldebug_info_start
	.byte 2,35,0,0,7
	.asciz "System_EventArgs"

	.long LTDIE_10 - Ldebug_info_start
LTDIE_10_POINTER:

	.byte 13
	.long LTDIE_10 - Ldebug_info_start
LTDIE_10_REFERENCE:

	.byte 14
	.long LTDIE_10 - Ldebug_info_start
LTDIE_11:

	.byte 5
	.asciz "_SpheroDeviceMessage"

	.byte 20,16
	.long LTDIE_5 - Ldebug_info_start
	.byte 2,35,0,6
	.asciz "timeStamp"

	.long LDIE_I8 - Ldebug_info_start
	.byte 2,35,12,6
	.asciz "robotId"

	.long LDIE_STRING - Ldebug_info_start
	.byte 2,35,8,0,7
	.asciz "_SpheroDeviceMessage"

	.long LTDIE_11 - Ldebug_info_start
LTDIE_11_POINTER:

	.byte 13
	.long LTDIE_11 - Ldebug_info_start
LTDIE_11_REFERENCE:

	.byte 14
	.long LTDIE_11 - Ldebug_info_start
LTDIE_9:

	.byte 5
	.asciz "_MessengerEventArgs"

	.byte 12,16
	.long LTDIE_10 - Ldebug_info_start
	.byte 2,35,0,6
	.asciz "message"

	.long LTDIE_11_REFERENCE - Ldebug_info_start
	.byte 2,35,8,0,7
	.asciz "_MessengerEventArgs"

	.long LTDIE_9 - Ldebug_info_start
LTDIE_9_POINTER:

	.byte 13
	.long LTDIE_9 - Ldebug_info_start
LTDIE_9_REFERENCE:

	.byte 14
	.long LTDIE_9 - Ldebug_info_start
LTDIE_13:

	.byte 8
	.asciz "_SpheroNotificationType"

	.byte 4
	.long LDIE_I4 - Ldebug_info_start
	.byte 9
	.asciz "CONNECTED"

	.byte 0,9
	.asciz "DISCONNECTED"

	.byte 1,9
	.asciz "CONNECTION_FAILED"

	.byte 2,0,7
	.asciz "_SpheroNotificationType"

	.long LTDIE_13 - Ldebug_info_start
LTDIE_13_POINTER:

	.byte 13
	.long LTDIE_13 - Ldebug_info_start
LTDIE_13_REFERENCE:

	.byte 14
	.long LTDIE_13 - Ldebug_info_start
LTDIE_12:

	.byte 5
	.asciz "_SpheroDeviceNotification"

	.byte 24,16
	.long LTDIE_11 - Ldebug_info_start
	.byte 2,35,0,6
	.asciz "m_NotificationType"

	.long LTDIE_13 - Ldebug_info_start
	.byte 2,35,20,0,7
	.asciz "_SpheroDeviceNotification"

	.long LTDIE_12 - Ldebug_info_start
LTDIE_12_POINTER:

	.byte 13
	.long LTDIE_12 - Ldebug_info_start
LTDIE_12_REFERENCE:

	.byte 14
	.long LTDIE_12 - Ldebug_info_start
	.byte 2
	.asciz "UISample:ReceiveNotificationMessage"
	.long Lm_5
	.long Lme_5

	.byte 2,118,16,3
	.asciz "this"

	.long LTDIE_0_REFERENCE - Ldebug_info_start
	.byte 2,123,8,3
	.asciz "sender"

	.long LDIE_OBJECT - Ldebug_info_start
	.byte 2,123,12,3
	.asciz "eventArgs"

	.long LTDIE_9_REFERENCE - Ldebug_info_start
	.byte 2,123,16,11
	.asciz "message"

	.long LTDIE_12_REFERENCE - Ldebug_info_start
	.byte 1,85,11
	.asciz "notifiedSphero"

	.long LTDIE_6_REFERENCE - Ldebug_info_start
	.byte 1,84,0

.section __DWARF, __debug_frame,regular,debug

	.long Lfde5_end - Lfde5_start
Lfde5_start:

	.long 0
	.align 2
	.long Lm_5

	.long Lme_5 - Lm_5
	.byte 12,13,0,76,14,8,135,2,68,14,36,132,9,133,8,134,7,136,6,139,5,140,4,142,3,68,14,72,68,13,11
	.align 2
Lfde5_end:

.section __DWARF, __debug_info,regular,debug
LTDIE_14:

	.byte 5
	.asciz "System_Array"

	.byte 8,16
	.long LTDIE_5 - Ldebug_info_start
	.byte 2,35,0,0,7
	.asciz "System_Array"

	.long LTDIE_14 - Ldebug_info_start
LTDIE_14_POINTER:

	.byte 13
	.long LTDIE_14 - Ldebug_info_start
LTDIE_14_REFERENCE:

	.byte 14
	.long LTDIE_14 - Ldebug_info_start
	.byte 2
	.asciz "(wrapper managed-to-native) System.Array:GetGenericValueImpl"
	.long Lm_7
	.long Lme_7

	.byte 2,118,16,3
	.asciz "this"

	.long LTDIE_14_REFERENCE - Ldebug_info_start
	.byte 2,123,8,3
	.asciz "param0"

	.long LDIE_I4 - Ldebug_info_start
	.byte 2,123,12,3
	.asciz "param1"

	.long LDIE_I - Ldebug_info_start
	.byte 2,123,16,0

.section __DWARF, __debug_frame,regular,debug

	.long Lfde6_end - Lfde6_start
Lfde6_start:

	.long 0
	.align 2
	.long Lm_7

	.long Lme_7 - Lm_7
	.byte 12,13,0,72,14,40,132,10,133,9,134,8,135,7,136,6,137,5,138,4,139,3,140,2,142,1,68,14,168,1,68,13
	.byte 11
	.align 2
Lfde6_end:

.section __DWARF, __debug_info,regular,debug

	.byte 0
Ldebug_info_end:
.text
	.align 3
mem_end:
#endif
