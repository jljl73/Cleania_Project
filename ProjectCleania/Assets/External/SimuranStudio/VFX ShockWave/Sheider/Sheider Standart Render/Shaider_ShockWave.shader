// Shader created with Shader Forge v1.38 
// Shader Forge (c) Freya Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:2,bsrc:3,bdst:7,dpts:2,wrdp:False,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0,fgcg:0,fgcb:0,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:True,fnfb:True,fsmp:False;n:type:ShaderForge.SFN_Final,id:4795,x:34326,y:32426,varname:node_4795,prsc:2|emission-9697-OUT,alpha-3095-OUT;n:type:ShaderForge.SFN_Power,id:2211,x:32012,y:32493,varname:node_2211,prsc:2|VAL-175-R,EXP-9791-OUT;n:type:ShaderForge.SFN_ValueProperty,id:9791,x:31785,y:32579,ptovrint:False,ptlb:Power_1,ptin:_Power_1,varname:node_9791,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_Tex2d,id:175,x:31477,y:32354,ptovrint:False,ptlb:MainTex,ptin:_MainTex,varname:_MainTex_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:d4aef365e36a1b748963b8506441bc86,ntxv:3,isnm:False|UVIN-1609-OUT;n:type:ShaderForge.SFN_Power,id:4825,x:32012,y:32721,varname:node_4825,prsc:2|VAL-143-R,EXP-9532-OUT;n:type:ShaderForge.SFN_Power,id:7127,x:32000,y:32986,varname:node_7127,prsc:2|VAL-1923-G,EXP-3144-OUT;n:type:ShaderForge.SFN_Power,id:6030,x:32000,y:33216,varname:node_6030,prsc:2|VAL-2840-G,EXP-6759-OUT;n:type:ShaderForge.SFN_ValueProperty,id:9532,x:31789,y:32861,ptovrint:False,ptlb:Power_2,ptin:_Power_2,varname:_Power_2,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:2;n:type:ShaderForge.SFN_ValueProperty,id:6759,x:31789,y:33352,ptovrint:False,ptlb:Power_4,ptin:_Power_4,varname:_Power_4,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:4;n:type:ShaderForge.SFN_Multiply,id:1511,x:32279,y:32620,varname:node_1511,prsc:2|A-2211-OUT,B-4825-OUT;n:type:ShaderForge.SFN_Multiply,id:802,x:32493,y:32814,varname:node_802,prsc:2|A-1511-OUT,B-4322-OUT;n:type:ShaderForge.SFN_ValueProperty,id:4322,x:32264,y:32794,ptovrint:False,ptlb:Multiply_1,ptin:_Multiply_1,varname:_Power_3,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_Multiply,id:4630,x:32236,y:33087,varname:node_4630,prsc:2|A-7127-OUT,B-6030-OUT;n:type:ShaderForge.SFN_Multiply,id:66,x:32505,y:32956,varname:node_66,prsc:2|A-4630-OUT,B-1356-OUT;n:type:ShaderForge.SFN_ValueProperty,id:1356,x:32162,y:33250,ptovrint:False,ptlb:Multiply_2,ptin:_Multiply_2,varname:_Multiply_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_Add,id:6802,x:32670,y:32835,varname:node_6802,prsc:2|A-802-OUT,B-66-OUT;n:type:ShaderForge.SFN_Clamp01,id:9116,x:32887,y:32835,varname:node_9116,prsc:2|IN-6802-OUT;n:type:ShaderForge.SFN_Power,id:6724,x:33375,y:32436,varname:node_6724,prsc:2|VAL-9116-OUT,EXP-8958-OUT;n:type:ShaderForge.SFN_ValueProperty,id:8958,x:33088,y:32600,ptovrint:False,ptlb:Main_Power,ptin:_Main_Power,varname:node_8958,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_ValueProperty,id:8199,x:33349,y:32604,ptovrint:False,ptlb:Main_Multiply,ptin:_Main_Multiply,varname:_Main_Power_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_ValueProperty,id:4479,x:31560,y:31007,ptovrint:False,ptlb:inner_Y,ptin:_inner_Y,varname:node_4479,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0.6;n:type:ShaderForge.SFN_Vector1,id:5834,x:31549,y:30889,varname:node_5834,prsc:2,v1:0;n:type:ShaderForge.SFN_Append,id:5228,x:31762,y:30889,varname:node_5228,prsc:2|A-5834-OUT,B-4479-OUT;n:type:ShaderForge.SFN_ComponentMask,id:9355,x:32097,y:30766,varname:node_9355,prsc:2,cc1:1,cc2:-1,cc3:-1,cc4:-1|IN-3240-OUT;n:type:ShaderForge.SFN_Power,id:4760,x:32375,y:30821,varname:node_4760,prsc:2|VAL-9355-OUT,EXP-7494-OUT;n:type:ShaderForge.SFN_ValueProperty,id:7494,x:32055,y:30974,ptovrint:False,ptlb:Inner_Power,ptin:_Inner_Power,varname:node_7494,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:20;n:type:ShaderForge.SFN_Add,id:3240,x:31929,y:30717,varname:node_3240,prsc:2|A-501-UVOUT,B-5228-OUT;n:type:ShaderForge.SFN_TexCoord,id:501,x:31273,y:31020,varname:node_501,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Clamp01,id:9238,x:32557,y:31011,varname:node_9238,prsc:2|IN-4760-OUT;n:type:ShaderForge.SFN_Vector1,id:2724,x:31560,y:31220,varname:node_2724,prsc:2,v1:0;n:type:ShaderForge.SFN_Append,id:349,x:31742,y:31310,varname:node_349,prsc:2|A-2724-OUT,B-2094-OUT;n:type:ShaderForge.SFN_ValueProperty,id:2094,x:31511,y:31339,ptovrint:False,ptlb:outer_Y,ptin:_outer_Y,varname:node_2094,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0.6;n:type:ShaderForge.SFN_Add,id:4068,x:31928,y:31227,varname:node_4068,prsc:2|A-3908-OUT,B-349-OUT;n:type:ShaderForge.SFN_ComponentMask,id:7644,x:32118,y:31276,varname:node_7644,prsc:2,cc1:1,cc2:-1,cc3:-1,cc4:-1|IN-4068-OUT;n:type:ShaderForge.SFN_OneMinus,id:3908,x:31726,y:31138,varname:node_3908,prsc:2|IN-501-UVOUT;n:type:ShaderForge.SFN_Power,id:7788,x:32334,y:31359,varname:node_7788,prsc:2|VAL-7644-OUT,EXP-5377-OUT;n:type:ShaderForge.SFN_Clamp01,id:3955,x:32597,y:31285,varname:node_3955,prsc:2|IN-7788-OUT;n:type:ShaderForge.SFN_ValueProperty,id:5377,x:31953,y:31432,ptovrint:False,ptlb:outer_Power,ptin:_outer_Power,varname:node_5377,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:20;n:type:ShaderForge.SFN_Add,id:1521,x:31989,y:31778,varname:node_1521,prsc:2|A-4616-OUT,B-4554-OUT;n:type:ShaderForge.SFN_Time,id:7679,x:31138,y:31856,varname:node_7679,prsc:2;n:type:ShaderForge.SFN_ValueProperty,id:2015,x:31138,y:32018,ptovrint:False,ptlb:Animate_time,ptin:_Animate_time,varname:node_2015,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_Multiply,id:8954,x:31456,y:31939,varname:node_8954,prsc:2|A-7679-TSL,B-2015-OUT;n:type:ShaderForge.SFN_Frac,id:2583,x:31633,y:31922,varname:node_2583,prsc:2|IN-8954-OUT;n:type:ShaderForge.SFN_ConstantLerp,id:4554,x:31802,y:31912,varname:node_4554,prsc:2,a:0,b:1|IN-2583-OUT;n:type:ShaderForge.SFN_ComponentMask,id:5267,x:32182,y:31789,varname:node_5267,prsc:2,cc1:0,cc2:0,cc3:-1,cc4:-1|IN-1521-OUT;n:type:ShaderForge.SFN_Multiply,id:2594,x:32415,y:31822,varname:node_2594,prsc:2|A-5267-G,B-8171-OUT;n:type:ShaderForge.SFN_Vector1,id:8171,x:32198,y:31956,varname:node_8171,prsc:2,v1:0.5;n:type:ShaderForge.SFN_Power,id:3521,x:32669,y:31914,varname:node_3521,prsc:2|VAL-2594-OUT,EXP-1954-OUT;n:type:ShaderForge.SFN_ValueProperty,id:1954,x:32438,y:32014,ptovrint:False,ptlb:Mask_power_Anim,ptin:_Mask_power_Anim,varname:node_1954,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:5;n:type:ShaderForge.SFN_SwitchProperty,id:4388,x:33043,y:31909,ptovrint:False,ptlb:Animate,ptin:_Animate,varname:_Animate_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,on:False|A-8836-OUT,B-3521-OUT;n:type:ShaderForge.SFN_ValueProperty,id:3144,x:31857,y:33111,ptovrint:False,ptlb:Power_3,ptin:_Power_3,varname:node_3144,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:3;n:type:ShaderForge.SFN_Tex2d,id:1923,x:31474,y:33019,ptovrint:False,ptlb:MainTex_3,ptin:_MainTex_3,varname:_MainTex_copy_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:d4aef365e36a1b748963b8506441bc86,ntxv:0,isnm:False|UVIN-5303-OUT;n:type:ShaderForge.SFN_Tex2d,id:2840,x:31358,y:33298,ptovrint:False,ptlb:MainTex_4,ptin:_MainTex_4,varname:_MainTex_copy_copy_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:d4aef365e36a1b748963b8506441bc86,ntxv:0,isnm:False|UVIN-8422-OUT;n:type:ShaderForge.SFN_Tex2d,id:143,x:31465,y:32695,ptovrint:False,ptlb:MainTex_2,ptin:_MainTex_2,varname:_MainTex_4,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:d4aef365e36a1b748963b8506441bc86,ntxv:0,isnm:False|UVIN-8410-OUT;n:type:ShaderForge.SFN_TexCoord,id:9971,x:29976,y:32469,varname:node_9971,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Time,id:882,x:30508,y:32239,varname:node_882,prsc:2;n:type:ShaderForge.SFN_Add,id:1609,x:31255,y:32281,varname:node_1609,prsc:2|A-7363-UVOUT,B-65-OUT;n:type:ShaderForge.SFN_Multiply,id:65,x:30956,y:32320,varname:node_65,prsc:2|A-882-TSL,B-5696-X;n:type:ShaderForge.SFN_Vector4Property,id:5696,x:30387,y:32459,ptovrint:False,ptlb:Speed_2,ptin:_Speed_2,varname:_Speed_2,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1,v2:1,v3:1,v4:1;n:type:ShaderForge.SFN_Multiply,id:2500,x:33118,y:31538,varname:node_2500,prsc:2|A-9238-OUT,B-3955-OUT;n:type:ShaderForge.SFN_Multiply,id:7159,x:33604,y:32539,varname:node_7159,prsc:2|A-6724-OUT,B-8199-OUT;n:type:ShaderForge.SFN_Time,id:1516,x:30560,y:32787,varname:node_1516,prsc:2;n:type:ShaderForge.SFN_Add,id:8410,x:31197,y:32725,varname:node_8410,prsc:2|A-7363-UVOUT,B-1835-OUT;n:type:ShaderForge.SFN_Multiply,id:1835,x:30894,y:32753,varname:node_1835,prsc:2|A-1516-TSL,B-5696-Y;n:type:ShaderForge.SFN_Time,id:3129,x:30690,y:33043,varname:node_3129,prsc:2;n:type:ShaderForge.SFN_Add,id:5303,x:31234,y:32986,varname:node_5303,prsc:2|A-7363-UVOUT,B-1042-OUT;n:type:ShaderForge.SFN_Multiply,id:1042,x:30935,y:33025,varname:node_1042,prsc:2|A-3129-TSL,B-5696-Z;n:type:ShaderForge.SFN_Time,id:8067,x:30713,y:33308,varname:node_8067,prsc:2;n:type:ShaderForge.SFN_Add,id:8422,x:31165,y:33272,varname:node_8422,prsc:2|A-7363-UVOUT,B-420-OUT;n:type:ShaderForge.SFN_Multiply,id:420,x:30958,y:33290,varname:node_420,prsc:2|A-8067-TSL,B-5696-W;n:type:ShaderForge.SFN_TexCoord,id:5647,x:31310,y:31655,varname:node_5647,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Multiply,id:2571,x:33345,y:31999,varname:node_2571,prsc:2|A-2500-OUT,B-4388-OUT;n:type:ShaderForge.SFN_Vector1,id:8836,x:32815,y:31840,varname:node_8836,prsc:2,v1:1;n:type:ShaderForge.SFN_OneMinus,id:4616,x:31605,y:31688,varname:node_4616,prsc:2|IN-5647-V;n:type:ShaderForge.SFN_TexCoord,id:1470,x:30797,y:33617,varname:node_1470,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Tex2d,id:6123,x:31527,y:33651,ptovrint:False,ptlb:MainTexure,ptin:_MainTexure,varname:_MainTex_5,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:d4aef365e36a1b748963b8506441bc86,ntxv:0,isnm:False|UVIN-211-OUT;n:type:ShaderForge.SFN_Power,id:9635,x:31974,y:33812,varname:node_9635,prsc:2|VAL-4549-OUT,EXP-7019-OUT;n:type:ShaderForge.SFN_ValueProperty,id:7019,x:31789,y:33919,ptovrint:False,ptlb:Power_Color,ptin:_Power_Color,varname:node_7019,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_Lerp,id:6008,x:32181,y:33689,varname:node_6008,prsc:2|A-3486-RGB,B-5033-RGB,T-9635-OUT;n:type:ShaderForge.SFN_Color,id:5033,x:31956,y:33463,ptovrint:False,ptlb:Color_1,ptin:_Color_1,varname:node_5033,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.9433962,c2:0.4227483,c3:0.879925,c4:1;n:type:ShaderForge.SFN_Color,id:3486,x:31839,y:33617,ptovrint:False,ptlb:Color_2,ptin:_Color_2,varname:_Color_2,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0,c2:1,c3:0.9717827,c4:1;n:type:ShaderForge.SFN_Multiply,id:9697,x:32417,y:33655,varname:node_9697,prsc:2|A-4625-OUT,B-6008-OUT;n:type:ShaderForge.SFN_ValueProperty,id:4625,x:32193,y:33497,ptovrint:False,ptlb:Emissive,ptin:_Emissive,varname:node_4625,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:2;n:type:ShaderForge.SFN_Multiply,id:3095,x:33848,y:32364,varname:node_3095,prsc:2|A-2571-OUT,B-7159-OUT;n:type:ShaderForge.SFN_Time,id:7317,x:30922,y:33863,varname:node_7317,prsc:2;n:type:ShaderForge.SFN_Multiply,id:7626,x:31225,y:33887,varname:node_7626,prsc:2|A-7317-TSL,B-2806-OUT;n:type:ShaderForge.SFN_Add,id:2806,x:30980,y:34027,varname:node_2806,prsc:2|A-7176-OUT,B-2140-OUT;n:type:ShaderForge.SFN_ValueProperty,id:7176,x:30724,y:34020,ptovrint:False,ptlb:speed x,ptin:_speedx,varname:node_7176,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_ValueProperty,id:2140,x:30724,y:34105,ptovrint:False,ptlb:speed y,ptin:_speedy,varname:_speedx_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_Add,id:211,x:31333,y:33668,varname:node_211,prsc:2|A-7363-UVOUT,B-7626-OUT;n:type:ShaderForge.SFN_FaceSign,id:9260,x:31527,y:33852,varname:node_9260,prsc:2,fstp:0;n:type:ShaderForge.SFN_Multiply,id:4549,x:31699,y:33762,varname:node_4549,prsc:2|A-6123-RGB,B-9260-VFACE;n:type:ShaderForge.SFN_Rotator,id:7363,x:30213,y:32665,varname:node_7363,prsc:2|UVIN-9971-UVOUT,ANG-2168-OUT;n:type:ShaderForge.SFN_ValueProperty,id:2168,x:29884,y:32768,ptovrint:False,ptlb:Rotator,ptin:_Rotator,varname:node_2168,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0;proporder:9791-9532-6759-4322-1356-8958-8199-4479-7494-2094-5377-2015-1954-4388-3144-175-1923-2840-143-5696-6123-7019-5033-3486-4625-7176-2140-2168;pass:END;sub:END;*/

Shader "SimuranStudio/Shaider_shock" {
    Properties {
        _Power_1 ("Power_1", Float ) = 1
        _Power_2 ("Power_2", Float ) = 2
        _Power_4 ("Power_4", Float ) = 4
        _Multiply_1 ("Multiply_1", Float ) = 1
        _Multiply_2 ("Multiply_2", Float ) = 1
        _Main_Power ("Main_Power", Float ) = 1
        _Main_Multiply ("Main_Multiply", Float ) = 1
        _inner_Y ("inner_Y", Float ) = 0.6
        _Inner_Power ("Inner_Power", Float ) = 20
        _outer_Y ("outer_Y", Float ) = 0.6
        _outer_Power ("outer_Power", Float ) = 20
        _Animate_time ("Animate_time", Float ) = 1
        _Mask_power_Anim ("Mask_power_Anim", Float ) = 5
        [MaterialToggle] _Animate ("Animate", Float ) = 1
        _Power_3 ("Power_3", Float ) = 3
        _MainTex ("MainTex", 2D) = "bump" {}
        _MainTex_3 ("MainTex_3", 2D) = "white" {}
        _MainTex_4 ("MainTex_4", 2D) = "white" {}
        _MainTex_2 ("MainTex_2", 2D) = "white" {}
        _Speed_2 ("Speed_2", Vector) = (1,1,1,1)
        _MainTexure ("MainTexure", 2D) = "white" {}
        _Power_Color ("Power_Color", Float ) = 1
        _Color_1 ("Color_1", Color) = (0.9433962,0.4227483,0.879925,1)
        _Color_2 ("Color_2", Color) = (0,1,0.9717827,1)
        _Emissive ("Emissive", Float ) = 2
        _speedx ("speed x", Float ) = 1
        _speedy ("speed y", Float ) = 1
        _Rotator ("Rotator", Float ) = 0
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend SrcAlpha OneMinusSrcAlpha
            Cull Off
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform float _Power_1;
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform float _Power_2;
            uniform float _Power_4;
            uniform float _Multiply_1;
            uniform float _Multiply_2;
            uniform float _Main_Power;
            uniform float _Main_Multiply;
            uniform float _inner_Y;
            uniform float _Inner_Power;
            uniform float _outer_Y;
            uniform float _outer_Power;
            uniform float _Animate_time;
            uniform float _Mask_power_Anim;
            uniform fixed _Animate;
            uniform float _Power_3;
            uniform sampler2D _MainTex_3; uniform float4 _MainTex_3_ST;
            uniform sampler2D _MainTex_4; uniform float4 _MainTex_4_ST;
            uniform sampler2D _MainTex_2; uniform float4 _MainTex_2_ST;
            uniform float4 _Speed_2;
            uniform sampler2D _MainTexure; uniform float4 _MainTexure_ST;
            uniform float _Power_Color;
            uniform float4 _Color_1;
            uniform float4 _Color_2;
            uniform float _Emissive;
            uniform float _speedx;
            uniform float _speedy;
            uniform float _Rotator;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                UNITY_FOG_COORDS(1)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
////// Lighting:
////// Emissive:
                float node_7363_ang = _Rotator;
                float node_7363_spd = 1.0;
                float node_7363_cos = cos(node_7363_spd*node_7363_ang);
                float node_7363_sin = sin(node_7363_spd*node_7363_ang);
                float2 node_7363_piv = float2(0.5,0.5);
                float2 node_7363 = (mul(i.uv0-node_7363_piv,float2x2( node_7363_cos, -node_7363_sin, node_7363_sin, node_7363_cos))+node_7363_piv);
                float4 node_7317 = _Time;
                float2 node_211 = (node_7363+(node_7317.r*(_speedx+_speedy)));
                float4 _MainTexure_var = tex2D(_MainTexure,TRANSFORM_TEX(node_211, _MainTexure));
                float3 emissive = (_Emissive*lerp(_Color_2.rgb,_Color_1.rgb,pow((_MainTexure_var.rgb*isFrontFace),_Power_Color)));
                float3 finalColor = emissive;
                float4 node_7679 = _Time;
                float4 node_882 = _Time;
                float2 node_1609 = (node_7363+(node_882.r*_Speed_2.r));
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(node_1609, _MainTex));
                float4 node_1516 = _Time;
                float2 node_8410 = (node_7363+(node_1516.r*_Speed_2.g));
                float4 _MainTex_2_var = tex2D(_MainTex_2,TRANSFORM_TEX(node_8410, _MainTex_2));
                float4 node_3129 = _Time;
                float2 node_5303 = (node_7363+(node_3129.r*_Speed_2.b));
                float4 _MainTex_3_var = tex2D(_MainTex_3,TRANSFORM_TEX(node_5303, _MainTex_3));
                float4 node_8067 = _Time;
                float2 node_8422 = (node_7363+(node_8067.r*_Speed_2.a));
                float4 _MainTex_4_var = tex2D(_MainTex_4,TRANSFORM_TEX(node_8422, _MainTex_4));
                fixed4 finalRGBA = fixed4(finalColor,(((saturate(pow((i.uv0+float2(0.0,_inner_Y)).g,_Inner_Power))*saturate(pow(((1.0 - i.uv0)+float2(0.0,_outer_Y)).g,_outer_Power)))*lerp( 1.0, pow((((1.0 - i.uv0.g)+lerp(0,1,frac((node_7679.r*_Animate_time)))).rr.g*0.5),_Mask_power_Anim), _Animate ))*(pow(saturate((((pow(_MainTex_var.r,_Power_1)*pow(_MainTex_2_var.r,_Power_2))*_Multiply_1)+((pow(_MainTex_3_var.g,_Power_3)*pow(_MainTex_4_var.g,_Power_4))*_Multiply_2))),_Main_Power)*_Main_Multiply)));
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
    }
    CustomEditor "ShaderForgeMaterialInspector"
}
