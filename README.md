<!-- PROJECT SHIELDS -->

[![Contributors][contributors-shield]][contributors-url]
[![Forks][forks-shield]][forks-url]
[![Stargazers][stars-shield]][stars-url]
[![Issues][issues-shield]][issues-url]
[![MIT License][license-shield]][license-url]
[![LinkedIn][linkedin-shield]][linkedin-url]

<!-- PROJECT LOGO -->
<br />
<div align="center">
  <a href="https://github.com/github_username/repo_name">
    <img src="https://connect-prd-cdn.unity.com/20200522/learn/images/7945f26d-c1a9-4b8e-952c-d7d44f82a7ae_Project1.png" alt="Logo" width="200">
  </a>

<h3 align="center">Humming birds</h3>

  <p align="center">
    In this project, intelligent flying hummingbirds can navigate to flowers, dip their beaks in, and drink nectar. These hummingbirds have six degrees of freedom, meaning they can fly and turn in any direction to find targets. They have more complicated controls and their flight paths cannot be solved with traditional navigation systems. You will learn how to craft a training environment and train neural networks to perform this challenging task, then compete against the birds yourself in a simple mini-game.
    <br />
    <a href="https://github.com/jprussoibanez/humming-birds"><strong>Explore the repo »</strong></a>
    <br />
    <br />
    <a href="https://learn.unity.com/course/ml-agents-hummingbirds">View Unity Course</a>
    ·
    <a href="https://github.com/jprussoibanez/humming-birds/issues">Report Bug</a>
    ·
    <a href="https://github.com/jprussoibanez/humming-birds/issues">Request Feature</a>
  </p>
</div>

<!-- TABLE OF CONTENTS -->
<details>
  <summary>Table of Contents</summary>
  <ol>
    <li>
      <a href="#about-the-project">About The Project</a>
      <ul>
        <li><a href="#built-with">Built With</a></li>
      </ul>
    </li>
    <li>
      <a href="#getting-started">Getting Started</a>
      <ul>
        <li><a href="#prerequisites">Prerequisites</a></li>
        <li><a href="#installation">Installation</a></li>
      </ul>
    </li>
    <li><a href="#usage">Usage</a></li>
    <li><a href="#contributing">Contributing</a></li>
    <li><a href="#license">License</a></li>
    <li><a href="#contact">Contact</a></li>
    <li><a href="#acknowledgments">Acknowledgments</a></li>
  </ol>
</details>

<!-- ABOUT THE PROJECT -->

## About The Project

[![Mini game][product-screenshot]](https://learn.unity.com/course/ml-agents-hummingbirds)

<p align="right">(<a href="#top">back to top</a>)</p>

### Built With

- [Unity](https://unity.com/)
- [VS code](https://code.visualstudio.com/)
- [ML agents](https://github.com/Unity-Technologies/ml-agents)

<p align="right">(<a href="#top">back to top</a>)</p>

<!-- GETTING STARTED -->

## Getting Started

This is an example of how you may give instructions on setting up your project locally.
To get a local copy up and running follow these simple example steps.

### Prerequisites

This is an example of how to list things you need to use the software and how to install them.

Download and install [VS code](https://code.visualstudio.com/), [Unity](https://unity.com/), [python 3.8](https://www.python.org/) & [poetry](https://python-poetry.org/).

### Installation

1. Clone the repo
   ```sh
   git clone https://github.com/jprussoibanez/humming-birds.git
   ```
2. Install python packages
   ```sh
   poetry install
   ```

<p align="right">(<a href="#top">back to top</a>)</p>

<!-- USAGE EXAMPLES -->

## Usage

### Training

For training, the [ml agents package](https://docs.unity3d.com/Packages/com.unity.ml-agents@2.0/manual/index.html) should be installed. Run the yaml configuration `mlagents-learn` command.

```sh
mlagents-learn ./ml_agents/humming_birds.yaml --run-id {run_id}
```

For a more detailed explanation take a look at the [course training module](https://learn.unity.com/project/training?courseId=5e470160edbc2a15578b13d7).

### Mini-game

For running the mini-game run the flower islands scene within the unity IDE.

For a more detailed explanation take a look at the [course mini-game module](https://learn.unity.com/project/minigame?courseId=5e470160edbc2a15578b13d7).

<p align="right">(<a href="#top">back to top</a>)</p>

<!-- CONTRIBUTING -->

## Contributing

Contributions are what make the open source community such an amazing place to learn, inspire, and create. Any contributions you make are **greatly appreciated**.

If you have a suggestion that would make this better, please fork the repo and create a pull request. You can also simply open an issue with the tag "enhancement".
Don't forget to give the project a star! Thanks again!

1. Fork the Project
2. Create your Feature Branch (`git checkout -b feature/AmazingFeature`)
3. Commit your Changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the Branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

<p align="right">(<a href="#top">back to top</a>)</p>

<!-- LICENSE -->

## License

Distributed under the MIT License. See `LICENSE.txt` for more information.

<p align="right">(<a href="#top">back to top</a>)</p>

<!-- CONTACT -->

## Contact

Juan Pablo Russo - [@jprussoibanez](https://twitter.com/jprussoibanez)

Project Link: [https://github.com/jprussoibanez/humming-birds](https://github.com/jprussoibanez/humming-birds)

<p align="right">(<a href="#top">back to top</a>)</p>

<!-- ACKNOWLEDGMENTS -->

## Acknowledgments

- [Unity course](https://learn.unity.com/course/ml-agents-hummingbirds)
- [Adam Kelly instructor](https://www.immersivelimit.com/)
<p align="right">(<a href="#top">back to top</a>)</p>

<!-- MARKDOWN LINKS & IMAGES -->
<!-- https://www.markdownguide.org/basic-syntax/#reference-style-links -->

[contributors-shield]: https://img.shields.io/github/contributors/jprussoibanez/humming-birds.svg?style=for-the-badge
[contributors-url]: https://github.com/jprussoibanez/humming-birds/graphs/contributors
[forks-shield]: https://img.shields.io/github/forks/jprussoibanez/humming-birds.svg?style=for-the-badge
[forks-url]: https://github.com/jprussoibanez/humming-birds/network/members
[stars-shield]: https://img.shields.io/github/stars/jprussoibanez/humming-birds.svg?style=for-the-badge
[stars-url]: https://github.com/jprussoibanez/humming-birds/stargazers
[issues-shield]: https://img.shields.io/github/issues/jprussoibanez/humming-birds.svg?style=for-the-badge
[issues-url]: https://github.com/jprussoibanez/humming-birds/issues
[license-shield]: https://img.shields.io/github/license/jprussoibanez/humming-birds.svg?style=for-the-badge
[license-url]: https://github.com/jprussoibanez/humming-birds/blob/master/LICENSE.txt
[linkedin-shield]: https://img.shields.io/badge/-LinkedIn-black.svg?style=for-the-badge&logo=linkedin&colorB=555
[linkedin-url]: https://www.linkedin.com/in/juanpablorusso/
[product-screenshot]: images/mini-game.png
