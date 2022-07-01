# Loan-Web-Api

Loan-Web-Api is an Api where client can register apply for 3 type of loans(Fast loan, Car loan and Buy with Credit) in 2 different currency (GEL and USD), the time the client can apply for a loan is counted in days, client can apply for the loan for minimum 6 month's which is 180 days and maximum for 5 years which is 1825 days, these are the limits for timeframe of the loan. Client also can modify loan while it is on processing, if the loan will be approved or declined after that decision from the accountant from the bank will make you can not modify it, plus you can add more loan or delete the old ones, still if they are on processing status. Also user can see his/her loans that he/she has applied previously. Also the web Api is giving the opportunity for the Accountant which has been already registered to the Database, he/she can log in and get all loans by Id of the user, delete and modify which means either it will be approved(some other details might be changed as well such as currency amount and time period) or declined. Also Accountant can block and unblock the clients, which means that if the client will be blocked he/she won't be able to apply for loans or modify or do any of the previous services which was explained.
Hope you will enjoy it!

## Using of Api
If you are a client and want to use the services which I have explained previously, you have to register first
and you will see this kind of fields that must be fullfiled:


## User Services:

## User Registration and requirements:
```java
Web page
(https://localhost:44397/api/User/register)

{  
  "name": "string",---(Name can not be empty and should contain 1-20 elements)
  "surname": "string",---(Surname can not be empty and should contain 1-20 elements)
  "username": "string",---(UserName can not be empty and should contain 1-20 elements)
  "password": "string",---(Your password must be minimum 8 characters)
  "monthlySalary": 0,---(Your salary should be more or equal to 500)
  "age": 0---(You must be more than 18 to apply for a loan)
}

```
## User Login and requirements::
```java

Web page
(https://localhost:44397/api/User/login)

{  
  "username": "string",
  "password": "string",
}

---(Password or Username was Empty and it has to be equal what you have used for registration)

```

## Add Loan service for User who is already authorized:
```java

Web page
(https://localhost:44397/api/Loan/Addloan)

{
  "loanType": (string",---"loan Type must not be null and you have to write : BuyWithCredit, FastLoan or CarLoan)
  "currency": (string",---"currency must not be null and you have to write :GEL or USD)
  "amount": 0,---(Amount must be greater than 100 and less than 1 000 000)
  "loanTime": 0---(Loan time frame should be from minimum 180 days to 1825 days(5 years) maximum)
}


```

## Modify Loan service for User who is already authorized and has registered loan which is still on proccessing:
```java

Web page
(https://localhost:44397/api/Loan/ModifyLoan)

{
  "amount": 0,---(Amount must be greater than 100 and less than 1 000 000)
  "currency": "string",---(currency must not be null and you have to write :GEL or USD)
  "loanType": "string",--- (string",---"loan Type must not be null and you have to write : BuyWithCredit, FastLoan or CarLoan)
  "loanTime": 0---(Loan time frame should be from minimum 180 days to 1825 days(5 years) maximum)
}


```

## Get Loan information service for User who is already authorized and has registered loan which is still on proccessing:
```java

Web page
(https://localhost:44397/api/Loan/getAllLoans)


Get all Loan service is the service which is used to check and see all loans that have been registered under your name and once you will call this service it will show you loan info such as currency , date, amount,type and loan id(which you can use for further services such as delete service or modify)

  (this service will show you the loans with information that have been registered from you)



```





## Delete Loan service for User who is already authorized and has registered loan which is still on proccessing:
```java

Web page
(https://localhost:44397/api/Loan/DeleteLoan)

{
  "loanId": 0--(Loan Id is the loan id which has been shown once you have added a loan previously and once you will indicate that loan id to the service it will give you situation to delete that loan)
}


```



```
```
## Accountant Services:

## Block user for loans:
```java
Web page
(https://localhost:44397/api/Accountant/BlockUserForNewLoan)


{  
    "userId": 0---(you have to indicate User Id which you want to be blocked for applying new loans)
}

```

## Unblock user for loans:
```java

Web page
(https://localhost:44397/api/Accountant/UnblockUser)

{  
    "userId": 0---(you have to indicate User Id which you want to be unblocked for applying new loans)
}

```


## Delete Loan by user ID:
```java

Web page
(https://localhost:44397/api/Accountant/DeleteLoanById)

{
  "loanId": 0---(you have to indicate User Id, from which you will be able to delete the loans or one of them that this specific user has registered)
}

```


## Modify Loan by Loan Id:
```java

Web page
(https://localhost:44397/api/Accountant/ModifyLoan)

{
  "loanId": 0,---(Loan Id is the id that you have to indicate for you to get the specific loan which you want to modify)
  "amount": 0,---(Amount must be greater than 100 and less than 1 000 000)
  "currency": "string",---(currency must not be null and you have to write :GEL or USD)
  "loanType": "string",--- (string",---"loan Type must not be null and you have to write : BuyWithCredit, FastLoan or CarLoan)
  "loanTime": 0---(Loan time frame should be from minimum 180 days to 1825 days(5 years) maximum)
}


(Some specific Example)
"result": [
        {
            "id": 16,
            "userId": 3,
            "loanAmount": 2000,
            "loanCurrency": "usd",
            "loanStatus": "processing",
            "loanType": "car loan",
            "loanTime": 500,
            "user": null
        },

```

##  Get Loan by user ID:
```java

{
  "userId": 0---(you have to indicate User Id, from which you will be able to Get the loans that this specific user has registered)
}

```


## Thanks for appreciating this application!!!
