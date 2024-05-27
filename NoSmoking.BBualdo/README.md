# NoSmoking
### This is my first Angular application and first FullStack Application as well.

## Configuration

1. Go to ```cd NoSmoking.BBualdo.UI``` and run ```npm install```.
2. In API folder find **appsetting.json** file and replace **ConnectionString:Default=secret** with your Database ConnectionString.
3. In **Package Manager Console** run ```Add-Migration Initial``` and ```Update-Database```.
4. In UI folder go to **smoke.service.ts** and replace **private url** string with your localhost:port/api/SmokeLogs


## Things to improve

- [ ] Merge separate Edit and Add Modals into one, reusable modal.
- [ ] Add loading spinner or other loading indicator while performing API operations.
- [ ] Merge logs with the same date and in detail view show each log by hours.
- [ ] Create functionality to track user's streak without smoking and visually appealing indication.