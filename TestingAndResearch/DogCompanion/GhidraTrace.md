This is what I have traced so far let me know if we need more.
```csharp
void FUN_180007bb0(longlong *param_1){
  uint uVar1;
  ulonglong uVar2;
  ulonglong local_res8;
  
  uVar1 = *(uint *)(*param_1 + 4);
  nativeInit(0xe88f4d7f52a6090f);
  local_res8 = (ulonglong)uVar1;
  nativePush64(local_res8);
  nativePush64(0x180049ea8);
  nativePush64(0);
  nativeCall();
  uVar2 = FUN_180016740((undefined8 *)"InvincibleDog");
  if ((char)uVar2 != '\0') {
    uVar1 = *(uint *)(*param_1 + 4);
    nativeInit(0xa5c38736c426fcb8);
    local_res8 = (ulonglong)uVar1;
    nativePush64(local_res8);
    nativePush64(0);
    nativeCall();
  }
  *(undefined4 *)(*param_1 + 4) = 0;
  FUN_180006030((uint *)(*param_1 + 0x68));
  FUN_18000c8b0((longlong)DAT_1800647a0,(undefined8 *)"model",&PTR_1800495b8);
  FUN_18000c8b0((longlong)DAT_1800647a0,(undefined8 *)&DAT_180049ebc,&PTR_1800495b8);
  FUN_18000c8b0((longlong)DAT_1800647a0,(undefined8 *)"bonding_level",&PTR_1800495b8);
  FUN_18000c8b0((longlong)DAT_1800647a0,(undefined8 *)"max_health",&PTR_1800495b8);
  (**(code **)(*DAT_1800647a0 + 8))(DAT_1800647a0,0);
  FUN_180014870(*(uint **)(*param_1 + 0x28));
  FUN_180014870(*(uint **)(*param_1 + 0x18));
  FUN_180014870(*(uint **)(*param_1 + 0x20));
  FUN_180014870(*(uint **)(*param_1 + 0x10));
  FUN_180014870(*(uint **)(*param_1 + 0x38));
  FUN_180014870(*(uint **)(*param_1 + 0x30));
  if (*(uint **)(*param_1 + 0x40) != (uint *)0x0) {
    FUN_180014870(*(uint **)(*param_1 + 0x40));
  }
  FUN_180013050(0x18004a428);
  return;
}

/* WARNING: Function: __security_check_cookie replaced with injection: security_check_cookie */

void FUN_180008370(longlong *param_1,uint param_2){
  __uint64 _Var1;
  uint uVar2;
  char cVar3;
  __uint64 *p_Var4;
  uint *puVar5;
  undefined **ppuVar6;
  uint *puVar7;
  char *pcVar8;
  undefined1 auStack_78 [32];
  uint *local_58;
  uint local_50 [2];
  uint local_48;
  int local_40;
  ulonglong local_28;
  
  uVar2 = DAT_180065b80;
  local_28 = DAT_180063178 ^ (ulonglong)auStack_78;
  nativeInit(0x460bc76a0e10655e);
  puVar7 = (uint *)0x0;
  local_58 = (uint *)(ulonglong)uVar2;
  nativePush64((__uint64)local_58);
  p_Var4 = nativeCall();
  if ((int)*p_Var4 != 0) {
    return;
  }
  if (*(int *)(*param_1 + 4) != 0) {
    return;
  }
  nativeInit(0x57ec5fa4d4d6afca);
  nativePush64(0);
  local_58 = (uint *)(ulonglong)param_2;
  nativePush64((__uint64)local_58);
  nativePush64((__uint64)local_50);
  local_58 = (uint *)0x3;
  nativePush64(3);
  p_Var4 = nativeCall();
  if ((int)*p_Var4 == 0) {
    pcVar8 = "WARN: animalInteraction event has no data";
  }
  else {
    if (local_50[0] != DAT_180065b80) {
      return;
    }
    nativeInit(0xfd340785adf8cfb7);
    nativePush64(0x18004a1f0);
    p_Var4 = nativeCall();
    if (local_40 != (int)*p_Var4) {
      return;
    }
    cVar3 = FUN_180014060(local_48);
    if (cVar3 == '\0') {
      return;
    }
    *(uint *)*param_1 = local_48;
    if (*(uint **)(*param_1 + 8) != (uint *)0x0) {
      FUN_180014870(*(uint **)(*param_1 + 8));
    }
    puVar5 = (uint *)FUN_18001b6ac(0x20);
    local_58 = puVar5;
    if (puVar5 != (uint *)0x0) {
      nativeInit(0xfd340785adf8cfb7);
      nativePush64(0x180049ce8);
      p_Var4 = nativeCall();
      _Var1 = *p_Var4;
      ppuVar6 = FUN_18000bb90(DAT_1800647a8,(undefined8 *)"interaction.accompany.promptLabel");
      puVar7 = FUN_180014510(puVar5,(__uint64)ppuVar6,(uint)_Var1);
    }
    *(uint **)(*param_1 + 8) = puVar7;
    FUN_180014980(*(uint **)((uint *)*param_1 + 2),*(uint *)*param_1);
    FUN_180014a30(*(uint **)(*param_1 + 8));
    FUN_180014970(*(uint **)(*param_1 + 8),1);
    FUN_180008d80(param_1,(undefined8 *)"bond_2");
    pcVar8 = "bonding prompt available";
  }
  FUN_180013050((longlong)pcVar8);
  return;
}

/* WARNING: Function: __security_check_cookie replaced with injection: security_check_cookie */

void FUN_180008f70(longlong *param_1){
  uint *puVar1;
  longlong lVar2;
  __uint64 _Var3;
  uint uVar4;
  bool bVar5;
  char cVar6;
  uint uVar7;
  long lVar8;
  int iVar9;
  undefined4 uVar10;
  __uint64 *p_Var11;
  undefined8 *puVar12;
  undefined **ppuVar13;
  ulonglong uVar14;
  uint uVar15;
  float fVar16;
  undefined8 uVar17;
  float fVar18;
  undefined1 auStack_128 [32];
  ulonglong local_108;
  uint local_100 [2];
  __uint64 local_f8 [4];
  int local_d8 [4];
  uint local_c8;
  ulonglong local_58;
  
  local_58 = DAT_180063178 ^ (ulonglong)auStack_128;
  nativeInit(0x5ce8de5909565748);
  nativePush64(0);
  p_Var11 = nativeCall();
  uVar15 = 0;
  _Var3 = *p_Var11;
  if (0 < (int)_Var3) {
    do {
      nativeInit(0xa85e614430eff816);
      nativePush64(0);
      local_108 = (ulonglong)uVar15;
      nativePush64(local_108);
      p_Var11 = nativeCall();
      iVar9 = (int)*p_Var11;
      nativeInit(0xfd340785adf8cfb7);
      nativePush64(0x180049ed8);
      p_Var11 = nativeCall();
      if (iVar9 == (int)*p_Var11) {
        FUN_180008370(param_1,uVar15);
      }
      else {
        nativeInit(0xfd340785adf8cfb7);
        nativePush64(0x180049ef8);
        p_Var11 = nativeCall();
        if (iVar9 == (int)*p_Var11) {
          if ((*(int *)(*param_1 + 4) != 0) &&
             (((puVar1 = *(uint **)(*param_1 + 0x58), puVar1 == (uint *)0x0 ||
               (bVar5 = FUN_18000ef90(puVar1), !bVar5)) || (*(char *)(*param_1 + 0xa1) == '\0')))) {
            nativeInit(0x57ec5fa4d4d6afca);
            nativePush64(0);
            local_108 = (ulonglong)uVar15;
            nativePush64(local_108);
            nativePush64((__uint64)local_100);
            local_108 = 1;
            nativePush64(1);
            p_Var11 = nativeCall();
            uVar4 = local_100[0];
            if ((int)*p_Var11 != 0) {
              nativeInit(0x7d5b1f88e7504bba);
              local_108 = (ulonglong)uVar4;
              nativePush64(local_108);
              p_Var11 = nativeCall();
              if (((int)*p_Var11 != 0) && (iVar9 = FUN_180006f10(uVar4), iVar9 != 0)) {
                uVar7 = FUN_180006df0(uVar4);
                iVar9 = FUN_180007100(uVar7);
                if (iVar9 == 0) {
                  nativeInit(0x5594afe9de0c01b7);
                  local_108 = (ulonglong)uVar4;
                  nativePush64(local_108);
                  p_Var11 = nativeCall();
                  if ((int)*p_Var11 == 0) {
                    nativeInit(0x93c8b64deb84728c);
                    local_108 = (ulonglong)uVar4;
                    nativePush64(local_108);
                    p_Var11 = nativeCall();
                    if ((uint)*p_Var11 == DAT_180065b80) {
                      fVar16 = (float)FUN_1800134c0(DAT_180065b80,uVar4);
                      lVar8 = FUN_18000c0e0((longlong)DAT_1800647a0,(undefined8 *)"fetch_range_min")
                      ;
                      if ((((float)lVar8 <= fVar16) &&
                          (lVar8 = FUN_18000c0e0((longlong)DAT_1800647a0,
                                                 (undefined8 *)"fetch_range_max"),
                          fVar16 <= (float)lVar8)) &&
                         (uVar14 = FUN_180016740((undefined8 *)"MarkHuntedAnimals"),
                         (char)uVar14 != '\0')) {
                        FUN_18000dd50(*(uint **)(*param_1 + 0x58),uVar4,10000);
                        FUN_180008d80(param_1,(undefined8 *)"fetch");
                      }
                    }
                  }
                }
              }
            }
          }
        }
        else {
          nativeInit(0xfd340785adf8cfb7);
          nativePush64(0x180049f18);
          p_Var11 = nativeCall();
          if (iVar9 == (int)*p_Var11) {
            if ((((*(int *)(*param_1 + 4) != 0) &&
                 (puVar1 = *(uint **)(*param_1 + 0x58), puVar1 != (uint *)0x0)) &&
                (bVar5 = FUN_18000efb0(puVar1), !bVar5)) &&
               ((iVar9 = FUN_180006e30(0,uVar15,(__uint64)local_d8,10), uVar4 = local_c8, iVar9 != 0
                && (local_d8[0] == 0x22)))) {
              nativeInit(0xc346a546612c49a9);
              local_108 = (ulonglong)uVar4;
              nativePush64(local_108);
              p_Var11 = nativeCall();
              if ((int)*p_Var11 == 0) {
                FUN_180019050(local_f8,uVar4);
                puVar12 = FUN_18000f100(*(int **)(*param_1 + 0x58),uVar4);
                *(undefined8 **)(*param_1 + 0x60) = puVar12;
                FUN_180013050(0x18004a230);
                FUN_180008d80(param_1,(undefined8 *)"tracking");
              }
            }
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
    puVar6 = (undefined8 *)*local_100;
    if (uVar7 < local_b8) {
      uVar7 = local_b8;
    }
    uVar7 = uVar7 - (longlong)puVar6;
LAB_180013204:
    FUN_180003cf0((longlong *)&local_38,puVar6,uVar7);
  }
  else if (((local_b0 & 4) == 0) && (*local_e8 != 0)) {
    puVar6 = (undefined8 *)*local_108;
    uVar7 = ((longlong)*local_d0 - (longlong)puVar6) + *local_e8;
    goto LAB_180013204;
  }
  plVar4 = (longlong *)&local_38;
  if (0xf < local_20) {
    plVar4 = (longlong *)CONCAT71(uStack_37,local_38);
  }
  plVar4 = FUN_18000a830((longlong *)&PTR_DAT_180063050,(longlong)plVar4);
  FUN_18000a830(plVar4,0x18004a6c8);
  if (0xf < local_20) {
    pvVar2 = (LPVOID)CONCAT71(uStack_37,local_38);
    pvVar5 = pvVar2;
    if ((0xfff < local_20 + 1) &&
       (pvVar5 = *(LPVOID *)((longlong)pvVar2 + -8),
       0x1f < (ulonglong)((longlong)pvVar2 + (-8 - (longlong)pvVar5)))) {
                    /* WARNING: Subroutine does not return */
      FUN_18002397c();
    }
    thunk_FUN_180024e94(pvVar5);
  }
  FUN_180011fb0((longlong *)&PTR_DAT_180063050);
LAB_18001327c:
  FUN_18000f7a0(local_138);
  return;
}

/* WARNING: Function: __security_check_cookie replaced with injection: security_check_cookie */

ulonglong FUN_18000be40(longlong param_1,undefined8 *param_2){
  LPVOID pvVar1;
  long lVar2;
  undefined **ppuVar3;
  longlong *plVar4;
  ulong *puVar5;
  undefined4 extraout_var;
  LPVOID pvVar6;
  longlong *_Str;
  ulonglong uVar7;
  undefined1 auStack_58 [32];
  longlong *local_38;
  char local_30;
  undefined7 uStack_2f;
  longlong local_20;
  ulonglong local_18;
  ulonglong local_10;
  
  local_10 = DAT_180063178 ^ (ulonglong)auStack_58;
  ppuVar3 = FUN_18000bb90(param_1,param_2);
  local_20 = 0;
  local_18 = 0xf;
  local_30 = '\0';
  uVar7 = 0xffffffffffffffff;
  do {
    uVar7 = uVar7 + 1;
  } while (*(char *)((longlong)ppuVar3 + uVar7) != '\0');
  plVar4 = FUN_180003cf0((longlong *)&local_30,ppuVar3,uVar7);
  if (local_20 == 0) {
    if (0xf < local_18) {
      pvVar1 = (LPVOID)CONCAT71(uStack_2f,local_30);
      pvVar6 = pvVar1;
      if ((0xfff < local_18 + 1) &&
         (pvVar6 = *(LPVOID *)((longlong)pvVar1 + -8),
         0x1f < (ulonglong)((longlong)pvVar1 + (-8 - (longlong)pvVar6)))) {
LAB_18000bf8c:
                    /* WARNING: Subroutine does not return */
        FUN_18002397c();
      }
      plVar4 = (longlong *)thunk_FUN_180024e94(pvVar6);
    }
  }
  else {
    puVar5 = __doserrno();
    _Str = (longlong *)&local_30;
    if (0xf < local_18) {
      _Str = (longlong *)CONCAT71(uStack_2f,local_30);
    }
    *puVar5 = 0;
    lVar2 = strtol((char *)_Str,(char **)&local_38,10);
    plVar4 = (longlong *)CONCAT44(extraout_var,lVar2);
    if (_Str == local_38) {
                    /* WARNING: Subroutine does not return */
      FUN_1800195ec(0x18004a530);
    }
    if (*puVar5 == 0x22) {
                    /* WARNING: Subroutine does not return */
      FUN_180019634(0x18004a548);
    }
    if (0xf < local_18) {
      pvVar1 = (LPVOID)CONCAT71(uStack_2f,local_30);
      pvVar6 = pvVar1;
      if ((0xfff < local_18 + 1) &&
         (pvVar6 = *(LPVOID *)((longlong)pvVar1 + -8),
         0x1f < (ulonglong)((longlong)pvVar1 + (-8 - (longlong)pvVar6)))) goto LAB_18000bf8c;
      plVar4 = (longlong *)thunk_FUN_180024e94(pvVar6);
    }
    if ((lVar2 != -999) && (lVar2 != 0)) {
      return CONCAT71((int7)((ulonglong)plVar4 >> 8),1);
    }
  }
  return (ulonglong)plVar4 & 0xffffffffffffff00;
}

/* WARNING: Function: __security_check_cookie replaced with injection: security_check_cookie */
/* WARNING: Type propagation algorithm not settling */

undefined ** FUN_18000bb90(longlong param_1,undefined8 *param_2){
  char cVar1;
  undefined8 *puVar2;
  ulonglong uVar3;
  undefined1 *puVar4;
  ulonglong uVar5;
  uint uVar6;
  undefined8 *puVar7;
  undefined8 *puVar8;
  undefined1 *puVar9;
  undefined8 *puVar10;
  undefined **ppuVar11;
  ulonglong uVar12;
  ulonglong uVar13;
  undefined1 auStack_78 [32];
  longlong local_58 [2];
  ulonglong local_48;
  ulonglong local_40;
  ulonglong local_38;
  
  local_38 = DAT_180063178 ^ (ulonglong)auStack_78;
  local_48 = 0;
  local_40 = 0xf;
  local_58[0]._0_1_ = 0;
  uVar12 = 0xffffffffffffffff;
  do {
    uVar12 = uVar12 + 1;
  } while (*(char *)((longlong)param_2 + uVar12) != '\0');
  FUN_180003cf0(local_58,param_2,uVar12);
  uVar5 = local_40;
  uVar12 = local_48;
  puVar2 = *(undefined8 **)(param_1 + 0x18);
  puVar4 = (undefined1 *)CONCAT71(local_58[0]._1_7_,(undefined1)local_58[0]);
  cVar1 = *(char *)((longlong)puVar2[1] + 0x19);
  puVar10 = puVar2;
  puVar8 = (undefined8 *)puVar2[1];
  while (cVar1 == '\0') {
    puVar9 = (undefined1 *)local_58;
    if (0xf < uVar5) {
      puVar9 = puVar4;
    }
    puVar7 = puVar8 + 4;
    if (0xf < (ulonglong)puVar8[7]) {
      puVar7 = (undefined8 *)puVar8[4];
    }
    uVar3 = puVar8[6];
    uVar13 = uVar3;
    if (uVar12 < uVar3) {
      uVar13 = uVar12;
    }
    uVar6 = memcmp(puVar7,puVar9,uVar13);
    if (uVar6 == 0) {
      if (uVar3 < uVar12) {
        uVar6 = 0xffffffff;
      }
      else {
        uVar6 = (uint)(uVar12 < uVar3);
      }
    }
    if ((int)uVar6 < 0) {
      puVar7 = (undefined8 *)puVar8[2];
      puVar8 = puVar10;
    }
    else {
      puVar7 = (undefined8 *)*puVar8;
    }
    puVar10 = puVar8;
    puVar8 = puVar7;
    cVar1 = *(char *)((longlong)puVar7 + 0x19);
  }
  if (*(char *)((longlong)puVar10 + 0x19) == '\0') {
    puVar8 = puVar10 + 4;
    if (0xf < (ulonglong)puVar10[7]) {
      puVar8 = (undefined8 *)puVar10[4];
    }
    puVar9 = (undefined1 *)local_58;
    if (0xf < uVar5) {
      puVar9 = puVar4;
    }
    uVar3 = puVar10[6];
    uVar13 = uVar12;
    if (uVar3 < uVar12) {
      uVar13 = uVar3;
    }
    uVar6 = memcmp(puVar9,puVar8,uVar13);
    if (uVar6 == 0) {
      if (uVar12 < uVar3) {
        uVar6 = 0xffffffff;
      }
      else {
        uVar6 = (uint)(uVar3 < uVar12);
      }
    }
    if ((int)uVar6 < 0) goto LAB_18000bcd5;
  }
  else {
LAB_18000bcd5:
    puVar10 = puVar2;
  }
  if (puVar10 == puVar2) {
    ppuVar11 = &PTR_1800495b8;
LAB_18000bdc6:
    if (0xf < uVar5) {
      puVar9 = puVar4;
      if ((0xfff < uVar5 + 1) &&
         (puVar9 = *(undefined1 **)(puVar4 + 0xfffffffffffffff8),
         (undefined1 *)0x1f < puVar4 + (-8 - (longlong)puVar9))) {
                    /* WARNING: Subroutine does not return */
        FUN_18002397c();
      }
      thunk_FUN_180024e94(puVar9);
    }
    return ppuVar11;
  }
  cVar1 = *(char *)((longlong)puVar2[1] + 0x19);
  puVar10 = (undefined8 *)puVar2[1];
  while (cVar1 == '\0') {
    puVar9 = (undefined1 *)local_58;
    if (0xf < uVar5) {
      puVar9 = puVar4;
    }
    puVar8 = puVar10 + 4;
    if (0xf < (ulonglong)puVar10[7]) {
      puVar8 = (undefined8 *)puVar10[4];
    }
    uVar3 = puVar10[6];
    uVar13 = uVar3;
    if (uVar12 < uVar3) {
      uVar13 = uVar12;
    }
    uVar6 = memcmp(puVar8,puVar9,uVar13);
    if (uVar6 == 0) {
      if (uVar3 < uVar12) {
        uVar6 = 0xffffffff;
      }
      else {
        uVar6 = (uint)(uVar12 < uVar3);
      }
    }
    if ((int)uVar6 < 0) {
      puVar8 = (undefined8 *)puVar10[2];
      puVar10 = puVar2;
    }
    else {
      puVar8 = (undefined8 *)*puVar10;
    }
    puVar2 = puVar10;
    puVar10 = puVar8;
    cVar1 = *(char *)((longlong)puVar8 + 0x19);
  }
  if (*(char *)((longlong)puVar2 + 0x19) == '\0') {
    puVar10 = puVar2 + 4;
    if (0xf < (ulonglong)puVar2[7]) {
      puVar10 = (undefined8 *)puVar2[4];
    }
    puVar9 = (undefined1 *)local_58;
    if (0xf < uVar5) {
      puVar9 = puVar4;
    }
    uVar3 = puVar2[6];
    uVar13 = uVar12;
    if (uVar3 < uVar12) {
      uVar13 = uVar3;
    }
    uVar6 = memcmp(puVar9,puVar10,uVar13);
    if (uVar6 == 0) {
      if (uVar12 < uVar3) {
        uVar6 = 0xffffffff;
      }
      else {
        uVar6 = (uint)(uVar3 < uVar12);
      }
    }
    if (-1 < (int)uVar6) {
      ppuVar11 = (undefined **)(puVar2 + 8);
      if (0xf < (ulonglong)puVar2[0xb]) {
        ppuVar11 = (undefined **)*ppuVar11;
      }
      goto LAB_18000bdc6;
    }
  }
                    /* WARNING: Subroutine does not return */
  FUN_180019634(0x18004a6d0);
}

void FUN_180019634(longlong param_1){
  undefined8 local_28 [5];
  
  FUN_18001953c(local_28,param_1);
                    /* WARNING: Subroutine does not return */
  _CxxThrowException(local_28,(ThrowInfo *)&DAT_180062300);
}
```