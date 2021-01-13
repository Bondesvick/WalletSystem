# Wallet System API

## A wallet system for a product used in multiple countries

##### To Start, navigate into "CurrencyAPI/CurrencyAPI" and Run:
```
dotnet run  
```

----
[Deployed Swagger Documentation](https://walletsystemapi-heroku.herokuapp.com/swagger/index.html)
----
![GitHub Repo stars](https://img.shields.io/github/stars/Bondesvick/WalletSystem?style=flat-square) ![GitHub branch checks state](https://img.shields.io/github/checks-status/Bondesvick/Bondesvick/master?style=social)
---
### This system would only be accessible to authenticated users.
--------
### User types:
 1. Noob
  
    - Can only have a wallet in a single currency selected at signup (main).
    
    - All wallet funding in a different currency should be converted to the main currency.
    
    - All wallet withdrawals in a different currency should be converted to the main currency before transactions are approved.
    
    - All wallet funding has to be approved by an administrator.
    
    - Cannot change main currency.
    
2. Elite

    - Can have multiple wallets in different currencies with a main currency selected at signup.
    
    - Funding in a particular currency should update the wallet with that currency or create it.
    
    - Withdrawals in a currency with funds in the wallet of that currency should reduce the wallet balance for that currency.
    
    - Withdrawals in a currency without a wallet balance should be converted to the main currency and withdrawn.
    
    - Cannot change main currency.
    
3. Admin

    - Cannot have a wallet.
    
    - Cannot withdraw funds from any wallet.
    
    - Can fund wallets for Noob or Elite users in any currency.
    
    - Can change the main currency of any user.
    
    - Approves wallet funding for Noob users.
    
    - Can promote or demote Noobs or Elite users
