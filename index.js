const grid = document.querySelector("#team-container");
const teamtoevoegen = document.querySelector("#TeamToevoegenKnop");

let aantalRestaurantsTekst = document.querySelector("#teamstat1 strong");
let aantalLedenTekst = document.querySelector("#teamstat2 strong");

let aantalRestaurants = 8;
let aantalleden = 5;

const restaurant={
    "image": "imgtemplate.png",
    "alt": "Icoontje van restaurant",
    "titel": "R",
    "lid": "naam",
}

teamtoevoegen.addEventListener("click", () => {
    aantalRestaurants++;
    Restaurantsaanmaken(restaurant,aantalRestaurants,aantalleden);
    
    aantalRestaurantsTekst.textContent = (aantalRestaurants);
    aantalLedenTekst.textContent = (aantalRestaurants * aantalleden);
});

grid.addEventListener("click", (event) => {
    const sluitknop = event.target.closest(".team-sluiten");

    if (sluitknop) {
        const kaart = sluitknop.closest(".restaurant");
        kaart.remove();
        aantalRestaurants--;

        aantalRestaurantsTekst.textContent = (aantalRestaurants);
        aantalLedenTekst.textContent = (aantalRestaurants * aantalleden);
    }
});

const Restaurantsaanmaken = (restaurant,aantalRestaurants,aantalleden) => {

    grid.innerHTML = ""

    for(let i = 1; i <= aantalRestaurants; i++){
        const kaart = document.createElement("article");
        kaart.classList.add("restaurant");

        kaart.innerHTML = `<div class="restaurant-header">
                         <div class="imageblok"> <img src="${restaurant.image}" alt="${restaurant.alt} ${i}"></div>
                            <h3>R${i}</h3>
                            <button class = "team-sluiten"><b>x</b></button>
                            </div>

                            <section class="Teamnamen" id="leden-${i}"></section>`;

        grid.appendChild(kaart);


        const teamnamenSectie = document.querySelector(`#leden-${i}`);

        for (let j = 1; j <= aantalleden; j++) {
            const naam = document.createElement("p");
            naam.textContent = `${restaurant.lid} ${j}`;
            teamnamenSectie.appendChild(naam);
        }
    }
};

Restaurantsaanmaken(restaurant,aantalRestaurants,aantalleden);