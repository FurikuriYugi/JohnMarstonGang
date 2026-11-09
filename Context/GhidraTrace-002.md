<!-- GhidraTrace lines 251-500 of 1048 -->
```csharp
          }
          else {
            nativeInit(0xfd340785adf8cfb7);
            nativePush64(0x180049f38);
            p_Var11 = nativeCall();
            if (iVar9 == (int)*p_Var11) {
              FUN_180008580(param_1,uVar15);
            }
          }
        }
      }
      uVar15 = uVar15 + 1;
    } while ((int)uVar15 < (int)_Var3);
  }
  if (((*(int *)*param_1 != 0) && (puVar1 = *(uint **)((int *)*param_1 + 2), puVar1 != (uint *)0x0))
     && (cVar6 = FUN_180014880(puVar1), cVar6 != '\0')) {
    FUN_180013050(0x18004a258);
    FUN_180014870(*(uint **)(*param_1 + 8));
    FUN_18000c920((longlong)DAT_1800647a0,(undefined8 *)"preset_index",1);
    FUN_18000c920((longlong)DAT_1800647a0,(undefined8 *)"bonding_level",1);
    uVar15 = FUN_18000c0e0(DAT_1800647b0,(undefined8 *)"bonding.1.max_health");
    FUN_18000c920((longlong)DAT_1800647a0,(undefined8 *)"max_health",uVar15);
    FUN_18000c920((longlong)DAT_1800647a0,(undefined8 *)"will_attack_predators",1);
    ppuVar13 = FUN_18000bb90(DAT_1800647a8,(undefined8 *)"companion.defaultBlipName");
    FUN_18000c8b0((longlong)DAT_1800647a0,(undefined8 *)&DAT_180049ebc,ppuVar13);
    uVar15 = FUN_18000c0e0(DAT_1800647b0,(undefined8 *)"bonding.1.whistling_range");
    FUN_18000c920((longlong)DAT_1800647a0,(undefined8 *)"whistling_range",uVar15);
    FUN_18000c920((longlong)DAT_1800647a0,(undefined8 *)"fetch_range_min",0x14);
    FUN_18000c920((longlong)DAT_1800647a0,(undefined8 *)"fetch_range_max",0x46);
    FUN_18000c920((longlong)DAT_1800647a0,(undefined8 *)"health_core",100);
    FUN_180007180(param_1,*(uint *)*param_1);
    *(undefined4 *)*param_1 = 0;
    uVar15 = *(uint *)(*param_1 + 4);
    nativeInit(0xda76a9f39210d365);
    local_108 = (ulonglong)uVar15;
    nativePush64(local_108);
    p_Var11 = nativeCall();
    FUN_18000c920((longlong)DAT_1800647a0,(undefined8 *)"model",(uint)*p_Var11);
    (**(code **)(*DAT_1800647a0 + 8))(DAT_1800647a0,0);
    FUN_180008d80(param_1,(undefined8 *)"bond_3");
  }
  uVar15 = *(uint *)(*param_1 + 4);
  if ((uVar15 == 0) || (*(longlong *)(*param_1 + 0x58) == 0)) {
    uVar17 = FUN_18000be40(DAT_1800647b8,(undefined8 *)"bond_1");
    if ((char)uVar17 != '\0') {
      return;
    }
    uVar15 = FUN_1800144a0();
    cVar6 = FUN_180014060(uVar15);
    if (cVar6 == '\0') {
      return;
    }
    FUN_180008d80(param_1,(undefined8 *)"bond_1");
    return;
  }
  nativeInit(0xd42bd6eb2e0f1677);
  local_108 = (ulonglong)uVar15;
  nativePush64(local_108);
  p_Var11 = nativeCall();
  if ((int)*p_Var11 == 0) {
    FUN_180006030((uint *)(*param_1 + 0x68));
    uVar15 = FUN_180008a70();
    FUN_180007180(param_1,uVar15);
    return;
  }
  uVar15 = *(uint *)(*param_1 + 4);
  nativeInit(0x7d5b1f88e7504bba);
  local_108 = (ulonglong)uVar15;
  nativePush64(local_108);
  p_Var11 = nativeCall();
  if ((int)*p_Var11 != 0) {
    ppuVar13 = FUN_18000bb90(DAT_1800647a8,(undefined8 *)"tutorial.companion_died");
    thunk_FUN_1800101a0((longlong)ppuVar13,5000);
    FUN_180013050(0x18004a400);
    FUN_180007bb0(param_1);
    return;
  }
  if (*(char *)(*param_1 + 0xa2) == '\0') {
    nativeInit(0x217e9dc48139933d);
    p_Var11 = nativeCall();
    _Var3 = *p_Var11;
    nativeInit(0x7964097fce4c244b);
    local_108 = (ulonglong)(uint)_Var3;
    nativePush64(local_108);
    p_Var11 = nativeCall();
    if ((int)*p_Var11 != 0) {
      fVar16 = (float)FUN_1800134c0(DAT_180065b80,*(uint *)(*param_1 + 4));
      lVar8 = FUN_18000c0e0(DAT_1800647b0,(undefined8 *)"auto_retrieve.min_distance");
      if ((float)lVar8 <= fVar16) {
        nativeInit(0xb15cd1cf58771de1);
        p_Var11 = nativeCall();
        if ((int)*p_Var11 == 0) {
          FUN_180013050(0x180049f78);
          FUN_180006270(*param_1);
        }
      }
    }
  }
  fVar16 = (float)FUN_1800134c0(DAT_180065b80,*(uint *)(*param_1 + 4));
  lVar8 = FUN_18000c0e0((longlong)DAT_1800647a0,(undefined8 *)"whistling_range");
  *(bool *)(*param_1 + 0xa1) = fVar16 < (float)lVar8;
  uVar15 = DAT_180065b80;
  nativeInit(0xf162e133b4e7a675);
  local_108 = (ulonglong)uVar15;
  nativePush64(local_108);
  p_Var11 = nativeCall();
  _Var3 = *p_Var11;
  iVar9 = *(int *)(*param_1 + 4);
  nativeInit(0x9bb01e3834671191);
  nativePush64((longlong)iVar9);
  nativePush64((longlong)(int)_Var3);
  nativePush64(0);
  p_Var11 = nativeCall();
  if (((int)*p_Var11 == 0) && (fVar16 < 30.0)) {
    uVar15 = FUN_180006f50(DAT_180065b80);
    FUN_1800070a0(*(uint *)(*param_1 + 4),uVar15);
  }
  nativeInit(0xfd340785adf8cfb7);
  nativePush64(0x180049ce8);
  p_Var11 = nativeCall();
  _Var3 = *p_Var11;
  nativeInit(0x50f940259d3841e6);
  nativePush64(0);
  local_108 = (ulonglong)(uint)_Var3;
  nativePush64(local_108);
  p_Var11 = nativeCall();
  if ((int)*p_Var11 != 0) {
    FUN_180008580(param_1,0);
  }
  lVar2 = *param_1;
  iVar9 = FUN_180006d80(*(uint *)(lVar2 + 4),1);
  fVar18 = *(float *)(lVar2 + 0x6c) / (float)iVar9;
  fVar16 = FUN_18000e2c0(*(uint **)(*param_1 + 0x58));
  if (((fVar16 < fVar18) &&
      (uVar17 = FUN_18000bfb0(DAT_1800647b0,(undefined8 *)"low_health_threshold"),
      (float)uVar17 < fVar18)) &&
     (uVar17 = FUN_18000bfb0(DAT_1800647b0,(undefined8 *)"low_health_threshold"),
     fVar16 < (float)uVar17)) {
    ppuVar13 = FUN_18000bb90(DAT_1800647a8,(undefined8 *)"tutorial.companion_low_health");
    thunk_FUN_1800101a0((longlong)ppuVar13,5000);
  }
  if (*(longlong **)(*param_1 + 0x60) != (longlong *)0x0) {
    cVar6 = (**(code **)(**(longlong **)(*param_1 + 0x60) + 0x18))();
    if (cVar6 == '\0') {
      cVar6 = (*(code *)((undefined8 *)**(longlong **)(*param_1 + 0x60))[1])();
      if (cVar6 == '\0') {
        *(undefined8 *)(*param_1 + 0x60) = 0;
      }
      else {
        uVar17 = (**(code **)(**(longlong **)(*param_1 + 0x60) + 0x20))();
        *(undefined8 *)(*param_1 + 0x60) = uVar17;
      }
    }
    else {
      (**(code **)**(longlong **)(*param_1 + 0x60))();
    }
  }
  FUN_180009a70(param_1);
  FUN_180008690(param_1);
  FUN_180009e00(param_1);
  uVar14 = FUN_180016740((undefined8 *)"ShowCores");
  if ((char)uVar14 == '\0') goto LAB_1800099c0;
  uVar10 = FUN_18000ecb0(*(uint **)(*param_1 + 0x58));
  FUN_180006a70(*(longlong *)(*param_1 + 0x50),(uint)((char)uVar10 != '\0'));
  lVar2 = *param_1;
  iVar9 = FUN_1800144a0();
  if ((iVar9 == *(int *)(lVar2 + 4)) ||
     (uVar10 = FUN_18000ecb0(*(uint **)(lVar2 + 0x58)), (char)uVar10 != '\0')) {
LAB_18000997a:
    FUN_180006a90(*(longlong *)(*param_1 + 0x50));
    nativeInit(0xd4ee21b7cc7fd350);
    local_108 = 1;
    nativePush64(1);
    nativeCall();
  }
  else {
    fVar16 = FUN_18000e2c0(*(uint **)(*param_1 + 0x58));
    uVar17 = FUN_18000bfb0(DAT_1800647b0,(undefined8 *)"low_health_threshold");
    if (fVar16 < (float)uVar17) goto LAB_18000997a;
    FUN_180006a60(*(longlong *)(*param_1 + 0x50));
    FUN_180007140(0);
  }
  FUN_180006aa0(*(uint **)(*param_1 + 0x50));
LAB_1800099c0:
  nativeInit(0x217e9dc48139933d);
  p_Var11 = nativeCall();
  _Var3 = *p_Var11;
  nativeInit(0x7964097fce4c244b);
  local_108 = (ulonglong)(uint)_Var3;
  nativePush64(local_108);
  p_Var11 = nativeCall();
  *(bool *)(*param_1 + 0xa2) = (int)*p_Var11 != 0;
  return;
}

/* WARNING: Function: __security_check_cookie replaced with injection: security_check_cookie */

void FUN_180013050(longlong param_1){
  int iVar1;
  LPVOID pvVar2;
  tm *ptVar3;
  longlong *plVar4;
  LPVOID pvVar5;
  undefined8 *puVar6;
  ulonglong uVar7;
  undefined1 auStack_168 [32];
  undefined4 local_148;
  __time64_t local_140;
  longlong local_138 [2];
  longlong local_128 [4];
  longlong *local_108;
  longlong *local_100;
  longlong *local_e8;
  ulonglong *local_e0;
  int *local_d0;
  ulonglong local_b8;
  ulonglong local_b0;
  undefined1 local_38;
  undefined7 uStack_37;
  undefined8 local_28;
  ulonglong local_20;
  ulonglong local_18;
  
  local_18 = DAT_180063178 ^ (ulonglong)auStack_168;
  local_148 = 0;
  local_140 = _time64((__time64_t *)0x0);
  ptVar3 = FID_conflict:_localtime64((__time32_t *)&local_140);
  FUN_180010970(local_138,1);
  FUN_18000c700((longlong *)&PTR_DAT_180063050,(wchar_t *)"DogCompanion.log",8,0x40);
  if (DAT_1800630d8 == 0) goto LAB_18001327c;
  iVar1 = ptVar3->tm_year;
  plVar4 = FUN_18000a830(local_128,0x18004aa34);
  plVar4 = FUN_180010b70(plVar4,ptVar3->tm_mday);
  plVar4 = FUN_18000a830(plVar4,0x18004a4b0);
  plVar4 = FUN_180010b70(plVar4,ptVar3->tm_mon);
  plVar4 = FUN_18000a830(plVar4,0x18004a4b0);
  plVar4 = FUN_180010b70(plVar4,iVar1 + 0x76c);
  plVar4 = FUN_18000a830(plVar4,0x180049e70);
  plVar4 = FUN_180010b70(plVar4,ptVar3->tm_hour);
  plVar4 = FUN_18000a830(plVar4,0x18004aa30);
  plVar4 = FUN_180010b70(plVar4,ptVar3->tm_min);
  plVar4 = FUN_18000a830(plVar4,0x18004aa30);
  plVar4 = FUN_180010b70(plVar4,ptVar3->tm_sec);
  plVar4 = FUN_18000a830(plVar4,0x18004aa2c);
  FUN_18000a830(plVar4,param_1);
  local_28 = 0;
  local_20 = 0xf;
  local_38 = 0;
  local_148 = 2;
  if (((local_b0 & 2) == 0) && (uVar7 = *local_e0, uVar7 != 0)) {
```
