<!-- GhidraTrace lines 501-750 of 1048 -->
```csharp
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
```
