@echo off
chcp 65001 >nul
title XML Validator - Lab 2
cls

echo ========================================
echo   XML Validator - CST8259 Lab 2
echo ========================================
echo.

cd /d %~dp0

echo 正在檢查 .NET SDK...
dotnet --version >nul 2>&1
if errorlevel 1 (
    echo [錯誤] 找不到 .NET SDK！
    echo 請確認已安裝 .NET SDK 8.0 或更高版本
    echo.
    pause
    exit /b 1
)

echo .NET SDK 檢查通過
echo.

echo 正在還原專案套件...
dotnet restore
if errorlevel 1 (
    echo [錯誤] 專案套件還原失敗！
    echo.
    pause
    exit /b 1
)

echo.
echo ========================================
echo 正在啟動專案...
echo ========================================
echo.
echo 專案啟動後，請在瀏覽器中開啟顯示的網址
echo （通常是 https://localhost:5001）
echo.
echo 按 Ctrl+C 可以停止伺服器
echo ========================================
echo.

dotnet run

echo.
echo ========================================
echo 伺服器已停止
echo ========================================
pause
