using Moq;
using Xunit;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using SampleApi.Models;
using SampleApi.Interfaces.MovieInterfaces;
using SampleApi.Services.MovieServices;
using SampleApi.DTOs.Movies;
using SampleAPI.Validators;


public class MovieServiceTests
{
    //GetById
    [Fact]
    public async Task GetById_ReturnsNull_WhenIdDoesNotMatchSetup()
    {
        // Arrange
        var movie = new Movie { Id = "1", Title = "A", Year = 2000 };

        var repoMock = new Mock<IMovieRepository>();
        repoMock.Setup(r => r.GetById("1"))
                .ReturnsAsync(movie);

        var validatorMock = new Mock<IMovieValidator>();

        var service = new MovieService(repoMock.Object, validatorMock.Object);

        // Act
        var result = await service.GetById("10");  // ID not configured

        // Assert
        Assert.Null(result);

        repoMock.Verify(r => r.GetById("10"), Times.Once);
    }

    //GetAll
    [Fact]
    public async Task GetAll_ReturnsMappedDTOs_WithCorrectCount()
    {
        // Arrange
        var movies = new List<Movie>
        {
            new Movie { Id = "1", Title = "A", Year = 2000 },
            new Movie { Id = "2", Title = "B", Year = 2001 }
        };

        var movieRepositiory_Mock = new Mock<IMovieRepository>();
        movieRepositiory_Mock.Setup(r => r.GetAll())
                .ReturnsAsync(movies);

        var validator_Mock = new Mock<IMovieValidator>();
        // No validation is used in GetAll(), so no setup needed

        var movieService = new MovieService(movieRepositiory_Mock.Object, validator_Mock.Object);

        // Act
        var result = await movieService.GetAll();

        // Assert
        Assert.Equal(movies.Count, result.Count());
    }

    // Add
    [Fact]
    public async Task Add_AddsRecord_WhenValidationPasses()
    {
        // Arrange
        var movieDTOAdd = new MovieDTOAdd { Title = "A", Year = 2000 };

        var movieEntity = new Movie { Id = "1", Title = "A", Year = 2000 };

        var repoMock = new Mock<IMovieRepository>();
        repoMock.Setup(r => r.Add(It.IsAny<Movie>()))
                .Returns(Task.CompletedTask);   // repository returns Task, not DTO

        var validatorMock = new Mock<IMovieValidator>();
        validatorMock.Setup(v => v.Validate(It.IsAny<Movie>()))
                    .Returns(new ValidationResult
                    {
                        IsValid = true,
                        Errors = new List<string>()
                    });

        var service = new MovieService(repoMock.Object, validatorMock.Object);

        // Act
        var result = await service.Add(movieDTOAdd);

        // Assert
        Assert.True(result.Success);
        Assert.Empty(result.Errors);

        // Verify repository was called
        repoMock.Verify(r => r.Add(It.IsAny<Movie>()), Times.Once);

        // Verify validator was called
        validatorMock.Verify(v => v.Validate(It.IsAny<Movie>()), Times.Once);
    }

    [Fact]
    public async Task Add_ReturnsError_WhenYearIsBelow1800()
    {
        // Arrange
        var movieDTOAdd = new MovieDTOAdd { Title = "A", Year = 1700 };

        var validatorMock = new Mock<IMovieValidator>();
        validatorMock.Setup(v => v.Validate(It.IsAny<Movie>()))
                    .Returns(new ValidationResult
                    {
                        IsValid = false,
                        Errors = new List<string> { "The years must be greater than 1799" }
                    });

        var repoMock = new Mock<IMovieRepository>();

        var service = new MovieService(repoMock.Object, validatorMock.Object);

        // Act
        var result = await service.Add(movieDTOAdd);

        // Assert
        Assert.False(result.Success);
        Assert.Contains("The years must be greater than 1799", result.Errors);

        repoMock.Verify(r => r.Add(It.IsAny<Movie>()), Times.Never);
    }

    [Fact]
    public async Task Add_ReturnsError_WhenTitleIsMissing()
    {
        // Arrange
        var movieDTOAdd = new MovieDTOAdd { Title = "", Year = 2000 };

        var validatorMock = new Mock<IMovieValidator>();
        validatorMock.Setup(v => v.Validate(It.IsAny<Movie>()))
                    .Returns(new ValidationResult
                    {
                        IsValid = false,
                        Errors = new List<string> { "The Title of the Movie cannot be empty" }
                    });

        var repoMock = new Mock<IMovieRepository>();

        var service = new MovieService(repoMock.Object, validatorMock.Object);

        // Act
        var result = await service.Add(movieDTOAdd);

        // Assert
        Assert.False(result.Success);
        Assert.Contains("The Title of the Movie cannot be empty", result.Errors);

        repoMock.Verify(r => r.Add(It.IsAny<Movie>()), Times.Never);
    }

    [Fact]
    public async Task Add_ReturnsError_WhenYearIsZero()
    {
        // Arrange
        var movieDTOAdd = new MovieDTOAdd { Title = "A", Year = 0 };

        var validatorMock = new Mock<IMovieValidator>();
        validatorMock.Setup(v => v.Validate(It.IsAny<Movie>()))
                    .Returns(new ValidationResult
                    {
                        IsValid = false,
                        Errors = new List<string> { "The years must be greater than 1799" }
                    });

        var repoMock = new Mock<IMovieRepository>();

        var service = new MovieService(repoMock.Object, validatorMock.Object);

        // Act
        var result = await service.Add(movieDTOAdd);

        // Assert
        Assert.False(result.Success);
        Assert.Contains("The years must be greater than 1799", result.Errors);

        repoMock.Verify(r => r.Add(It.IsAny<Movie>()), Times.Never);
    }




}
