while ($true){
    Invoke-RestMethod -Uri 'https://xxxxxx/order-jobs/create-deliveries' -Method POST
    Start-Sleep -Seconds 1
    Invoke-RestMethod -Uri 'https://xxxxxx/donate-jobs/include-to-stock' -Method POST
    Start-Sleep -Seconds 1
    Invoke-RestMethod -Uri 'https://xxxxxx/order-jobs/recalc-percentages' -Method POST
    Start-Sleep -Seconds 5
}