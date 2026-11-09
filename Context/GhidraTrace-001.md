<!-- GhidraTrace lines 1-250 of 1048 -->
```csharp
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
```
