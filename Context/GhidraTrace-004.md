<!-- GhidraTrace lines 751-1000 of 1048 -->
```csharp
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

void FUN_180014a30(uint *param_1){
  *(undefined1 *)((longlong)param_1 + 5) = 1;
  FUN_180014a40(param_1);
  return;
}

uint * FUN_180014510(uint *param_1,__uint64 param_2,uint param_3){
  FUN_180014530(param_1,param_2,param_3,0);
  return param_1;
}

void FUN_180014980(uint *param_1,uint param_2){
  uint uVar1;
  __uint64 *p_Var2;
  ulonglong local_res8;
  
  param_1[5] = param_2;
  if (param_2 == 0) {
    uVar1 = *param_1;
    nativeInit(0x2f11d3a254169ea4);
    local_res8 = (ulonglong)uVar1;
    nativePush64(local_res8);
    nativePush64(0);
    nativePush64(0);
                    /* WARNING: Could not recover jumptable at 0x0001800149da. Too many branches */
                    /* WARNING: Treating indirect jump as call */
    nativeCall();
    return;
  }
  nativeInit(0xb796970bd125fce8);
  local_res8 = (ulonglong)param_2;
  nativePush64(local_res8);
  p_Var2 = nativeCall();
  if ((int)*p_Var2 != 0) {
    FUN_1800147f0(*param_1,(longlong)(int)*p_Var2,0);
  }
  return;
}
void FUN_180014970(uint *param_1,undefined1 param_2){
  *(undefined1 *)(param_1 + 1) = param_2;
  FUN_180014a40(param_1);
  return;
}

uint * FUN_180014530(uint *param_1,__uint64 param_2,uint param_3,uint param_4){
  uint uVar1;
  __uint64 *p_Var2;
  __uint64 _Var3;
  ulonglong local_res8;
  
  nativeInit(0x4f97de45a519419);
  p_Var2 = nativeCall();
  _Var3 = *p_Var2;
  *param_1 = (uint)_Var3;
  param_1[4] = param_3;
  nativeInit(0xb5352b7494a08258);
  local_res8 = (ulonglong)(uint)_Var3;
  nativePush64(local_res8);
  local_res8 = (ulonglong)param_3;
  nativePush64(local_res8);
  nativeCall();
  *(__uint64 *)(param_1 + 2) = param_2;
  nativeInit(0xfa925ac00eb830b9);
  nativePush64(10);
  nativePush64(0x180049a08);
  nativePush64(param_2);
  p_Var2 = nativeCall();
  uVar1 = *param_1;
  _Var3 = *p_Var2;
  nativeInit(0x5dd02a8318420dd7);
  local_res8 = (ulonglong)uVar1;
  nativePush64(local_res8);
  nativePush64(_Var3);
  nativeCall();
  uVar1 = *param_1;
  param_1[5] = 0;
  nativeInit(0x2f11d3a254169ea4);
  local_res8 = (ulonglong)uVar1;
  nativePush64(local_res8);
  nativePush64(0);
  nativePush64(0);
  nativeCall();
  param_1[6] = param_4;
  if (param_4 == 0) {
    _Var3 = 0xcc6656799977741b;
LAB_1800146ea:
    uVar1 = *param_1;
    nativeInit(_Var3);
    local_res8 = (ulonglong)uVar1;
    nativePush64(local_res8);
    _Var3 = 1;
  }
  else {
    if ((param_4 == 1) || (param_4 == 2)) {
      _Var3 = 0x94073d5ca3f16b7b;
      goto LAB_1800146ea;
    }
    if (param_4 != 3) goto LAB_180014718;
    uVar1 = *param_1;
    nativeInit(0x32df729d8bd3c1c6);
    local_res8 = (ulonglong)uVar1;
    nativePush64(local_res8);
    nativePush64(1);
    nativePush64(0);
    nativePush64(0);
    _Var3 = 0;
  }
  nativePush64(_Var3);
  nativeCall();
LAB_180014718:
  uVar1 = *param_1;
  nativeInit(0x560e76d5e2e1803f);
  local_res8 = (ulonglong)uVar1;
  nativePush64(local_res8);
  nativePush64(0x12);
  nativePush64(1);
  nativeCall();
  uVar1 = *param_1;
  nativeInit(0xf7aa2696a22ad8b9);
  local_res8 = (ulonglong)uVar1;
  nativePush64(local_res8);
  nativeCall();
  *(undefined1 *)(param_1 + 7) = 0;
  *(undefined2 *)(param_1 + 1) = 1;
  return param_1;
}

void FUN_180014a40(uint *param_1){
  uint uVar1;
  uint uVar2;
  ulonglong local_res8;
  
  uVar1 = *param_1;
  if (*(char *)((longlong)param_1 + 5) == '\0') {
    nativeInit(0x8a0fb4d03a630d21);
    local_res8 = (ulonglong)uVar1;
    nativePush64(local_res8);
    nativePush64(0);
    nativeCall();
    uVar1 = *param_1;
    nativeInit(0x71215accfde075ee);
    local_res8 = (ulonglong)uVar1;
    nativePush64(local_res8);
    nativePush64(0);
                    /* WARNING: Could not recover jumptable at 0x000180014b71. Too many branches */
                    /* WARNING: Treating indirect jump as call */
    nativeCall();
    return;
  }
  uVar2 = param_1[1];
  nativeInit(0x8a0fb4d03a630d21);
  local_res8 = (ulonglong)uVar1;
  nativePush64(local_res8);
  local_res8 = (ulonglong)(byte)uVar2;
  nativePush64(local_res8);
  nativeCall();
  uVar1 = *param_1;
  nativeInit(0x71215accfde075ee);
  local_res8 = (ulonglong)uVar1;
  nativePush64(local_res8);
  nativePush64(1);
                    /* WARNING: Could not recover jumptable at 0x000180014af9. Too many branches */
                    /* WARNING: Treating indirect jump as call */
  nativeCall();
  return;
}

void FUN_180014870(uint *param_1){
  *(undefined1 *)((longlong)param_1 + 5) = 0;
  FUN_180014a40(param_1);
  return;
}

undefined1 FUN_180014060(uint param_1){
  int iVar1;
  __uint64 *p_Var2;
  ulonglong local_res10;
  
  nativeInit(0x964000d355219fc0);
  local_res10 = (ulonglong)param_1;
  nativePush64(local_res10);
  p_Var2 = nativeCall();
  iVar1 = (int)*p_Var2;
  if (((((iVar1 == 0x59f5325d) || (iVar1 == 0x373ad87a)) || (iVar1 == -0x23fb3a33)) ||
      (((iVar1 == -0x4ed91048 || (iVar1 == -0x6148f29d)) ||
       ((iVar1 == -0x42de965b || ((iVar1 == 0x714fae8f || (iVar1 == 0xffdb5b2)))))))) ||
     ((iVar1 == 0x67d66c41 ||
      ((((iVar1 == 0x738f36b2 || (iVar1 == -0x76616d20)) || (iVar1 == 0x2ec2783a)) ||
       (iVar1 == -0x4f98dbc8)))))) {
    return 1;
  }
  return 0;
```
