@echo off
chcp 65001 >nul
title XML Validator - Lab 2
echo ========================================
echo   XML Validator - CST8259 Lab 2
echo ========================================
echo.
echo 正在啟動專案...
echo 請稍候，這可能需要幾秒鐘...
echo.
echo 專案啟動後，請在瀏覽器中開啟顯示的網址
echo （通常是 https://localhost:5001）
echo.
echo 按 Ctrl+C 可以停止伺服器
echo ========================================
echo.

cd /d %~dp0

dotnet run

echo.
echo ========================================
echo 伺服器已停止
echo ========================================
pause
