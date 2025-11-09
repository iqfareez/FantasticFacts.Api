![.Net](https://img.shields.io/badge/.NET%208-5C2D91?style=for-the-badge&logo=.net&logoColor=white)

# FantasticFacts Api

Get fantastic facts about various topics!

This project is created for me to practice deploying a .NET Web API application to production environment.

## Getting Started

```powershell
dotnet run --project FantasticFacts.Api
```

## Data Source

Few years ago when I still subscribed to [How-To Geek](https://www.howtogeek.com/) newsletter, they will include a random fact in their emails. I found that is interesting so I have collected and saved those facts in a [document](https://1drv.ms/w/c/a1f6618ce10a85b0/EbCFCuGMYfYggKG_aQAAAAABEq5ocJmPVIqpU7sfqZUCdA?e=AjmdkZ).

I document is a Word Document. I use markitdown to convert into [markdown](https://github.com/microsoft/markitdown), do a simple cleanup (eg removes image placeholders) and later is used in the database seeder of this application.

## Deployment

I've successfully deployed this app on my home server. The setup involves Docker, Portainer (as the Docker interface) and Cloudflare tunnels.

<img width="1575" height="508" alt="image" src="https://github.com/user-attachments/assets/7936a699-c496-4dca-ac55-dc357515afb9" />

<img width="1565" height="799" alt="image" src="https://github.com/user-attachments/assets/dd8b4342-f1ad-46ed-918a-5b471719ce99" />

<img width="1533" height="774" alt="image" src="https://github.com/user-attachments/assets/d6421d6c-8adf-47d6-95b8-bc7b4a8e3749" />

The App can be accessed via: https://fantasticfacts.iqfareez.com/swagger/index.html

<img width="1193" height="799" alt="image" src="https://github.com/user-attachments/assets/acea8ec9-ae23-45d2-8885-8741e105e09f" />

