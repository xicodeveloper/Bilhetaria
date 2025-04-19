function scrollContainerLeft() {
    console.log("scrollContainerLeft called");
    const container = document.getElementById('ScrollableContainer'); // Cambiado a getElementById
    const scrollAmount = 100; // Ajusta la cantidad de desplazamiento según sea necesario
    container.scrollBy({ left: -scrollAmount, behavior: 'smooth' }); // Cambiado a desplazamiento horizontal
}

function scrollContainerRight() {
    console.log("scrollContainerRight");
    const container = document.getElementById('ScrollableContainer'); // Cambiado a getElementById
    const scrollAmount = 100; // Ajusta la cantidad de desplazamiento según sea necesario
    container.scrollBy({ left: scrollAmount, behavior: 'smooth' }); // Cambiado a desplazamiento horizontal
}