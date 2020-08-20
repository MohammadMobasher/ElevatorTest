// loader component
class PriceTag extends HTMLElement {
    constructor() {
        super();
        let x = this.getAttribute('x') || 50;
        let y = this.getAttribute('y') || 0;
        let text = this.getAttribute('text') || "عنوان";
        let backgroundColor = this.getAttribute('backgroundColor') || "#000";
        let textColor = this.getAttribute('textColor') || "#fff";
        this.innerHTML = `
		<svg style="width: 90px;position:absolute;top:0;left:0;z-index:1" xmlns="http://www.w3.org/2000/svg" fill="${backgroundColor}" viewBox="0 0 436.572 426.368">
		<g id="tag-left" transform="translate(0.566 426.368) rotate(-90)">
			<path id="Path_1" data-name="Path 1" d="M8.452-.7,0,38.9H61.908l133.85-23.142Z" transform="translate(0 4.36)" fill="rgba(0,0,0,0.5)"/>
			<path id="Path_2" data-name="Path 2" d="M204.537,287.555l-39,8.6V105.731Z" transform="translate(218.957 139.851)" fill="rgba(0,0,0,0.5)"/>
			<path id="Path_3" data-name="Path 3" d="M448.2,321.027,812.611,693.85V665.644L476.09,321.027Z" transform="translate(-428.089 -277.636)" fill="rgba(0,0,0,0.1)"/>
			<path id="Path_3-2" data-name="Path 3" d="M428.727,290.521c-5.513.177-6.872,3.551-3.015,7.5L834.137,715.872c3.857,3.946,7.15,2.662,7.32-2.855l-.179-157.638a26.972,26.972,0,0,0-6.729-17.182L597.783,297.471a26.257,26.257,0,0,0-17.063-6.831l-137.944-.57Z" transform="translate(-415.089 -290.636)"/>
			<text id="PriceTag_Text" transform="translate(300 240) rotate(45)" fill="${textColor}" font-size="61"><tspan x="${x}" y="${y}">${text}</tspan></text>
		</g>
	  </svg>
	  `;
    }
}

// Define the new element
customElements.define('price-tag', PriceTag);

