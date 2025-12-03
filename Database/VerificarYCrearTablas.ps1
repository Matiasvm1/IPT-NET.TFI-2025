# Script para verificar y crear las tablas del módulo de Cobro de Cuotas
# Ejecutar desde PowerShell: .\Database\VerificarYCrearTablas.ps1

Write-Host "========================================" -ForegroundColor Cyan
Write-Host "Verificación de Base de Datos IvcDb" -ForegroundColor Cyan
Write-Host "========================================" -ForegroundColor Cyan
Write-Host ""

# Configuración de conexión
$ServerInstance = "."
$Database = "IvcDb"

# Verificar si existe la base de datos
Write-Host "1. Verificando existencia de base de datos IvcDb..." -ForegroundColor Yellow
$checkDb = "IF DB_ID('IvcDb') IS NOT NULL SELECT 'EXISTS' ELSE SELECT 'NOT EXISTS'"

try {
    $dbExists = Invoke-Sqlcmd -ServerInstance $ServerInstance -Query $checkDb -ErrorAction Stop
    
    if ($dbExists.Column1 -eq "NOT EXISTS") {
        Write-Host "   ? ERROR: La base de datos IvcDb no existe." -ForegroundColor Red
     Write-Host "   ? Debe crear la base de datos primero." -ForegroundColor Red
  exit 1
    }
    
    Write-Host "   ? Base de datos IvcDb existe." -ForegroundColor Green
    Write-Host ""
    
} catch {
    Write-Host "   ? ERROR al conectar con SQL Server: $_" -ForegroundColor Red
    Write-Host "   ? Verifique que SQL Server esté ejecutándose." -ForegroundColor Red
  exit 1
}

# Verificar tablas existentes
Write-Host "2. Verificando tablas existentes..." -ForegroundColor Yellow
$queryTablas = @"
SELECT TABLE_NAME 
FROM INFORMATION_SCHEMA.TABLES 
WHERE TABLE_TYPE = 'BASE TABLE'
ORDER BY TABLE_NAME
"@

try {
    $tablasExistentes = Invoke-Sqlcmd -ServerInstance $ServerInstance -Database $Database -Query $queryTablas
    
    Write-Host "   Tablas actuales en IvcDb:" -ForegroundColor Cyan
 foreach ($tabla in $tablasExistentes) {
      Write-Host "   - $($tabla.TABLE_NAME)" -ForegroundColor Gray
    }
    Write-Host ""
    
} catch {
    Write-Host "   ? ERROR al consultar tablas: $_" -ForegroundColor Red
    exit 1
}

# Verificar si ya existen las tablas del módulo de Cuotas
Write-Host "3. Verificando tablas del módulo de Cobro de Cuotas..." -ForegroundColor Yellow
$tablasRequeridas = @("Alumnos", "Cuotas", "PagosCuotas")
$tablasExistentesNombres = $tablasExistentes | Select-Object -ExpandProperty TABLE_NAME
$tablasFaltantes = @()

foreach ($tabla in $tablasRequeridas) {
    if ($tablasExistentesNombres -contains $tabla) {
        Write-Host "   ? Tabla '$tabla' ya existe." -ForegroundColor Green
    } else {
        Write-Host "   ? Tabla '$tabla' NO existe." -ForegroundColor Red
        $tablasFaltantes += $tabla
    }
}
Write-Host ""

# Si faltan tablas, ejecutar el script de creación
if ($tablasFaltantes.Count -gt 0) {
    Write-Host "4. Creando tablas faltantes..." -ForegroundColor Yellow
    
 $scriptPath = Join-Path $PSScriptRoot "01_CreateCobroCuotas.sql"
    
    if (-not (Test-Path $scriptPath)) {
    Write-Host "   ? ERROR: No se encontró el archivo '$scriptPath'" -ForegroundColor Red
        exit 1
    }
    
    Write-Host "   Ejecutando script: $scriptPath" -ForegroundColor Cyan
 
    try {
        # Leer el contenido del script
        $scriptContent = Get-Content $scriptPath -Raw
 
        # Ejecutar el script
    Invoke-Sqlcmd -ServerInstance $ServerInstance -Database $Database -Query $scriptContent -Verbose
        
        Write-Host "   ? Script ejecutado correctamente." -ForegroundColor Green
   Write-Host ""
     
    } catch {
    Write-Host "   ? ERROR al ejecutar el script: $_" -ForegroundColor Red
        exit 1
    }
    
} else {
    Write-Host "4. ? Todas las tablas del módulo ya existen." -ForegroundColor Green
    Write-Host ""
}

# Verificación final
Write-Host "5. Verificación final de datos..." -ForegroundColor Yellow

$queryVerificacion = @"
SELECT 'ALUMNOS' AS Tabla, COUNT(*) AS Total FROM Alumnos
UNION ALL
SELECT 'CUOTAS', COUNT(*) FROM Cuotas
UNION ALL
SELECT 'PAGOS_CUOTAS', COUNT(*) FROM PagosCuotas;
"@

try {
    $resultados = Invoke-Sqlcmd -ServerInstance $ServerInstance -Database $Database -Query $queryVerificacion
  
    Write-Host "   Resumen de datos:" -ForegroundColor Cyan
    foreach ($fila in $resultados) {
        $color = if ($fila.Total -gt 0) { "Green" } else { "Yellow" }
        Write-Host "   - $($fila.Tabla): $($fila.Total) registros" -ForegroundColor $color
    }
    Write-Host ""
    
} catch {
    Write-Host "   ?? No se pudo verificar los datos (es normal si las tablas están vacías)." -ForegroundColor Yellow
    Write-Host ""
}

Write-Host "========================================" -ForegroundColor Cyan
Write-Host "? Proceso completado exitosamente" -ForegroundColor Green
Write-Host "========================================" -ForegroundColor Cyan
Write-Host ""
Write-Host "Puede ejecutar su aplicación ahora:" -ForegroundColor Yellow
Write-Host "   dotnet run --project TFI.Vista" -ForegroundColor Cyan
Write-Host ""
