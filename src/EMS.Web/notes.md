Some libraries to check out
- https://github.com/fergaldoyle/vue-form
- https://github.com/logaretm/vee-validate
    - going to go with this one for validation
- https://github.com/probil/v-mask
- https://github.com/CodeSeven/toastr
- https://zerossl.com/ - used for ssl cert

Todo
- Get working in IE 9 - 11 (need promise (https://github.com/github/fetch/issues/114) and probably find (https://github.com/Spetnik/ie-Array.find)) - X
- Saving off a player is bringing back too many when returned from the api, why? - X
- Add coupon code for discount - X
- Add team checkout - X
  - Send confirmation email once paid - X
  - Add wait icon while paying - X
  - When the teams have been processed, need to remove them from the shopping cart list - X
- Allow for team/player deletion
- Add back in "My Account"
- Hook up cancel button and revert changes - X
- Add validation
  - Team validation - X
  - coupon code validation - X
  - Player validation - X
- Make things look pretty
  - Use the vuejs "no flash" trick - X
  - Make buttons on Team screen all look similar - X
- Force https - X
- Why is Add Player button so big on clicking surface
- Add logging
- Add Admin Features
  - Get reports working with custom ClosedXML (Open XML supports .net core, so just need to create custom version that has stripped down functionality)
- Look to see if you can do a computed field on $fields.dirty()

Vue tips and tricks
- v-for="(item, index) in items"

Using Let's Encrypt for generating signature
- navigate to https://zerossl.com/
- follow steps to generate the necessary files using Bash for Windows
    - can use existing account key (not domain key) and csr (request) to renew

Tips for startssl cert - looks like startcom is not trusted by FireFox or Chrome anymore (https://security.googleblog.com/2016/10/distrusting-wosign-and-startcom.html)
- https://www.startssl.com/ - used for ssl cert
- need to validate domain through email or ???
- valid now for 3 years
- create csr either using openssl from ubuntu on windows or by using IE
  - openssl req -newkey rsa:4096 -keyout stokesbaryme.key -out stokesbaryme.csr
- once cert is created, download file and extract iisserver zip file
- copy crt to location where key file was created using openssl
- openssl pkcs12 -export -out domain.name.pfx -inkey domain-key.txt -in domain-crt.txt


width: 270px