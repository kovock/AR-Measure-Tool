# 📏Measuring AR - Aplicativo de AR para Medição de Distância
Measuring AR é um aplicativo para o sistema Android que realiza medições de distância em tempo real por meio de Realidade Aumentada. A aplicação foi desenvolvida utilizando Unity 6, AR Foundation e ARCore.



## Visão Geral

O objetivo deste projeto é desenvolver uma aplicação para o sistema Android baseada em Realidade Aumentada, capaz de realizar medições de distância em tempo real em ambientes reais. A proposta do trabalho é aplicar, de forma prática, os conceitos estudados na disciplina de Computação Gráfica, incluindo rastreamento espacial, detecção de superfícies e interação entre o usuário e o ambiente.  
## Funcionalidades do aplicativo

* Detecção de superfícies planas no ambiente real por meio da câmera do dispositivo.  
* Posicionamento interativo de pontos de medição utilizando toques na tela.  
* Cálculo automático da distância entre os pontos selecionados.  
* Exibição da medida de distância em tempo real na interface do aplicativo.  
* Possibilidade de realizar múltiplas medições de forma consecutiva.  
* Interação intuitiva entre o usuário e o ambiente em Realidade Aumentada.

## Tecnologias Utilizadas
**Engine Unity 6**: responsável pela criação do ambiente gráfico e pela integração dos elementos de Realidade Aumentada

**AR Foundation**: utilizado como camada de abstração para funcionalidades de AR

**ARCore**: empregado para rastreamento de movimento, detecção de superfícies e compreensão do ambiente real em dispositivos Android

**Linguagem de Programação C\#**: utilizada para implementação da lógica do aplicativo; e o sistema operacional Android, plataforma alvo da aplicação.

## Guia de Instalação

**Opção A: Compilar o Código**

1. Instalar o Unity Hub no computador, pode fazer o download através do link: [https://unity.com/download](https://unity.com/download)  
2. No Unity Hub, instalar a versão Unity 6.0 (6000.0.65f1) utilizada no projeto, garantindo que os módulos de build para Android estejam selecionados (Android Build Support, SDK, NDK e OpenJDK).   
3. Clonar ou baixar o repositório do projeto Measuring AR para a máquina local  
4. Abrir o Unity Hub e adicionar a pasta do projeto “MedidorAR”  
5. Abra o projeto e aguarde carregar.  
6. Com o projeto aberto faça o passo a passo a seguir:  
   1. Para habilitar a cena, vá em “Assets” clique em “Scenes” e de um clique duplo em "CenaMedicaoAr", como na imagen abaixo:
<img width="1533" height="773" alt="Captura de tela 2026-01-18 145358" src="https://github.com/user-attachments/assets/021588e2-c8da-4262-873f-d9bc54a9a5b4" />

   2. Para executar a aplicação vamos acessar as configurações de Build Profiles e selecionar a plataforma Android.  
<img width="1075" height="799" alt="Captura de tela 2026-01-18 155056" src="https://github.com/user-attachments/assets/7e21d367-3588-41f4-abb3-955a6ef42d97" />

   3. Clique em “Android” e depois clique em “Switch Platform” para fazer a mudança.  
<img width="1347" height="703" alt="Captura de tela 2026-01-18 155344" src="https://github.com/user-attachments/assets/ed551d53-258c-4afe-be8e-c519ad5acaa2" />

   4. Conecte um dispositivo Android ao computador via USB.  
   5. Ative o Modo Desenvolvedor e a Depuração USB no dispositivo Android.  
   6. Clique em Run Device e procure pelo seu dispositivo android, caso não encontre clique em Refresh.
<img width="1337" height="706" alt="Captura de tela 2026-01-18 160434" src="https://github.com/user-attachments/assets/85fd0e57-6ae6-43ab-904d-3e836904d4c8" />

8. Para executar a aplicação clique em “file” e depois em “Build and Run” e Salve o Apk em uma pasta da sua preferência.  
 <img width="1068" height="419" alt="Captura de tela 2026-01-18 161030" src="https://github.com/user-attachments/assets/d741b86a-51fe-4518-9340-fcabe1cc3b05" />



   

**Opção B: APK**

1. Faça o download do APK no seu dispositivo android  
2. Se o Android bloquear, seleciona Definições \> Permitir desta fonte.  
3. Clica em Instalar.  
4. Abra o aplicativo e dê as permissões para utilização da câmera.  
5. Teste o Aplicativo

## Como Usar o Aplicativo

1. Abra o aplicativo.  
2. Conceda as permissões de acesso à câmera quando solicitado.  
3. Aponte a câmera do dispositivo para o ambiente ao redor até que o aplicativo detecte uma superfície plana.  
4. Toque na tela e arraste para posicionar o ponto de medição no espaço.  
5. A distância entre os pontos será calculada automaticamente e exibida na tela.  
6. Clique no botão com o ícone de lixeira para limpar as medidas.  
7. Caso deseje ocultar a malha da superfície, clique nos três pontos no canto superior direito e desative a opção “Visualize Surfaces”.
