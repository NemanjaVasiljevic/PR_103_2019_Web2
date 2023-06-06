import { initializeApp } from "firebase/app";
import { getAuth } from "firebase/auth";


const firebaseConfig = {

    apiKey: "AIzaSyBMNPedU0z38nCy10rLRyjAzazZQ7RvV64",
  
    authDomain: "web2-project-31110.firebaseapp.com",
  
    projectId: "web2-project-31110",
  
    storageBucket: "web2-project-31110.appspot.com",
  
    messagingSenderId: "258256503237",
  
    appId: "1:258256503237:web:c0187b0e2fa8e407d78bac",
  
    measurementId: "G-1KGM3D6EDG"
  
  }

const app = initializeApp(firebaseConfig);

export const auth = getAuth(app);
